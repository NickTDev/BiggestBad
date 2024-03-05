// Merle Roji

using System.Collections.Generic;
using UnityEngine;

namespace T02.BehaviorEditor
{
    [CreateAssetMenu(fileName = "Behavior Graph", menuName = "Behavior Editor/New Behavior Graph", order = 0)]
    public class BehaviorGraph : ScriptableObject
    {
        [SerializeField] public List<BaseNode> AllWindows = new List<BaseNode>();
        [SerializeField] public int IDCount;

        private List<int> _indexToDelete = new List<int>();

        #region Checkers

        /// <summary>
        /// Returns a Node with a given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BaseNode GetNodeWithIndex(int index)
        {
            for (int i = 0; i < AllWindows.Count; i++)
            {
                if (AllWindows[i].ID == index)
                    return AllWindows[i];
            }

            return null;
        }

        /// <summary>
        /// Delete all of the windows that need to be deleted.
        /// </summary>
        public void DeleteWindowsThatNeedTo()
        {
            for (int i = 0; i < _indexToDelete.Count; i++)
            {
                BaseNode baseNode = GetNodeWithIndex(_indexToDelete[i]);
                if (baseNode != null)
                    AllWindows.Remove(baseNode);
            }

            _indexToDelete.Clear();
        }

        /// <summary>
        /// Delete Node at given index.
        /// </summary>
        /// <param name="index"></param>
        public void DeleteNode(int index)
        {
            if (!_indexToDelete.Contains(index)) _indexToDelete.Add(index);
        }

        /// <summary>
        /// Checks to see if there are multiple of the same state node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsStateDuplicate(BaseNode node)
        {
            for (int i = 0; i < AllWindows.Count; i++)
            {
                if (AllWindows[i].ID == node.ID) continue;

                if (AllWindows[i].StateRef.CurrentState == node.StateRef.CurrentState &&
                    !AllWindows[i].IsDuplicate)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks to see if there are multiple of the same transition node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsTransitionDuplicate(BaseNode node)
        {
            BaseNode enter = GetNodeWithIndex(node.EnterNode);
            if (enter == null) return false;

            for (int i = 0; i < enter.StateRef.CurrentState.Transitions.Count; i++)
            {
                Transition transition = enter.StateRef.CurrentState.Transitions[i];
                if (transition.ConditionToPass == node.TransitionRef.PreviousCondition
                    && node.TransitionRef.TransitionID != transition.ID) return true;
            }

            return false;
        }

        #endregion
    }
}
