// Merle Roji

using UnityEngine;

namespace T02.BehaviorEditor
{
    [CreateAssetMenu(fileName = "BehaviorEditorSettings", menuName = "Behavior Editor/New Editor Settings")]
    public class BehaviorEditorSettings : ScriptableObject
    {
        public BehaviorGraph CurrentGraph;
        public StateNode StateNodeLogic;
        public PortalNode PortalNodeLogic;
        public TransitionNode TransitionNodeLogic;
        public CommentNode CommentNodeLogic;
        public bool ConnectTransition;
        public GUISkin ActiveSkin;

        /// <summary>
        /// Adds a node on the current behavior graph.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="title"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public BaseNode AddNodeOnGraph(DrawNode type, float width, float height, string title, Vector3 position)
        {
            BaseNode baseNode = new BaseNode();

            baseNode.NodeToDraw = type;
            baseNode.WindowRect.width = width;
            baseNode.WindowRect.height = height;
            baseNode.WindowRect.x = position.x;
            baseNode.WindowRect.y = position.y;
            baseNode.WindowTitle = title;
            CurrentGraph.AllWindows.Add(baseNode);
            baseNode.TransitionRef = new TransitionNodeReferences();
            baseNode.StateRef = new StateNodeReferences();
            baseNode.ID = CurrentGraph.IDCount;
            CurrentGraph.IDCount++;

            return baseNode;
        }
    }
}

