// Merle Roji

using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// A Node that transitions between State Nodes.
    /// </summary>
    [CreateAssetMenu(fileName = "TransitionNode", menuName = "Behavior Editor/Nodes/New Transition Node", order = 1)]
    public class TransitionNode : DrawNode
    {
        /// <summary>
        /// Initializes the transition.
        /// </summary>
        /// <param name="enterState"></param>
        /// <param name="transition"></param>
        public void Init(StateNode enterState, Transition transition)
        {
            // this.EnterState = enterState;
        }

        public override void DrawWindow(BaseNode baseNode)
        {
            EditorGUILayout.LabelField("");
            BaseNode enterNode = BehaviorEditor.s_settings.CurrentGraph.GetNodeWithIndex(baseNode.EnterNode);
            if (enterNode == null) return;
            if (enterNode.StateRef.CurrentState == null)
            {
                BehaviorEditor.s_settings.CurrentGraph.DeleteNode(baseNode.ID);
                return;
            }

            Transition transition = enterNode.StateRef.CurrentState.GetTransition(baseNode.TransitionRef.TransitionID);
            if (transition == null) return;

            transition.ConditionToPass = 
                (Condition)EditorGUILayout.ObjectField(transition.ConditionToPass, 
                typeof(Condition), false);

            if (transition.ConditionToPass == null)
            {
                EditorGUILayout.LabelField("No Condition!!");
                baseNode.IsAssigned = false;
            }
            else
            {
                baseNode.IsAssigned = true;
                if (baseNode.IsDuplicate)
                {
                    EditorGUILayout.LabelField("Duplicate Condition!!");
                }
                else
                {
                    GUILayout.Label(transition.ConditionToPass.Description);

                    BaseNode targetNode = BehaviorEditor.s_settings.CurrentGraph.GetNodeWithIndex(baseNode.TargetNode);
                    if (targetNode != null)
                    {
                        if (!targetNode.IsDuplicate)
                        {
                            transition.TargetState = targetNode.StateRef.CurrentState;
                        }
                        else
                        {
                            transition.TargetState = null;
                        }
                    }
                    else
                    {
                        transition.TargetState = null;
                    }
                }
            }

            if (baseNode.TransitionRef.PreviousCondition != transition.ConditionToPass)
            {
                baseNode.TransitionRef.PreviousCondition = transition.ConditionToPass;
                baseNode.IsDuplicate = BehaviorEditor.s_settings.CurrentGraph.IsTransitionDuplicate(baseNode);

                if (!baseNode.IsDuplicate)
                {
                    BehaviorEditor.s_forceSetDirty = true;
                    //BehaviorEditor.s_settings.CurrentGraph.SetNode(this);
                }
            }
        }

        public override void DrawCurve(BaseNode baseNode)
        {
            Rect rect = baseNode.WindowRect;
            rect.y += baseNode.WindowRect.height * 0.5f;
            rect.width = 1;
            rect.height = 1;

            BaseNode enterTransition = BehaviorEditor.s_settings.CurrentGraph.GetNodeWithIndex(baseNode.EnterNode);
            if (enterTransition == null)
            {
                BehaviorEditor.s_settings.CurrentGraph.DeleteNode(baseNode.ID);
            }
            else
            {
                Color targetColor = Color.green; // color if everything is working fine
                if (!baseNode.IsAssigned || baseNode.IsDuplicate)
                {
                    targetColor = Color.red;
                }

                Rect enterRect = enterTransition.WindowRect;
                BehaviorEditor.DrawNodeCurve(enterRect, rect, true, targetColor);
            }

            if (baseNode.IsDuplicate) return;

            if (baseNode.TargetNode > 0)
            {
                BaseNode exitTransition = BehaviorEditor.s_settings.CurrentGraph.GetNodeWithIndex(baseNode.TargetNode);
                if (exitTransition == null)
                {
                    baseNode.TargetNode = -1;
                }
                else
                {
                    rect = baseNode.WindowRect;
                    rect.x += rect.width;
                    
                    Rect endRect = exitTransition.WindowRect;
                    endRect.x -= endRect.width * 0.5f;

                    Color targetColor = Color.green; // color if everything is working fine

                    if (exitTransition.NodeToDraw is StateNode)
                    {
                        if (!exitTransition.IsAssigned || exitTransition.IsDuplicate) targetColor = Color.red;
                    }
                    else
                    {
                        if (!exitTransition.IsAssigned) targetColor = Color.red;
                        else targetColor = Color.yellow;
                    }

                    BehaviorEditor.DrawNodeCurve(rect, endRect, false, targetColor);
                }
            }
        }
    }
}
