// Merle Roji

using UnityEngine;
using UnityEditor;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// The Portal Node is similar to a transition, but instead warps back to a previous state.
    /// </summary>
    [CreateAssetMenu(fileName = "Portal Node", menuName = "Behavior Editor/Nodes/New Portal Node", order = 3)]
    public class PortalNode : DrawNode
    {
        public override void DrawCurve(BaseNode baseNode)
        {
            
        }

        public override void DrawWindow(BaseNode baseNode)
        {
            baseNode.StateRef.CurrentState = (State)EditorGUILayout.ObjectField(baseNode.StateRef.CurrentState, typeof(State), false);
            baseNode.IsAssigned = baseNode.StateRef.CurrentState != null;

            if (baseNode.StateRef.PreviousState != baseNode.StateRef.CurrentState)
            {
                baseNode.StateRef.PreviousState = baseNode.StateRef.CurrentState;
                BehaviorEditor.s_forceSetDirty = true;
            }
        }
    }
}
