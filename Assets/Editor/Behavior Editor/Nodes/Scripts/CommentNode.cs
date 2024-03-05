// Merle Roji

using UnityEngine;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// A Node that contains a comment.
    /// </summary>
    [CreateAssetMenu(fileName = "CommentNode", menuName = "Behavior Editor/Nodes/New Comment Node", order = 2)]
    public class CommentNode : DrawNode
    {
        public override void DrawWindow(BaseNode baseNode)
        {
            baseNode.Comment = GUILayout.TextArea(baseNode.Comment, 200);
        }

        public override void DrawCurve(BaseNode baseNode)
        {
            
        }
    }
}