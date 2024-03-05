// Merle Roji

using UnityEngine;

namespace T02.BehaviorEditor
{
    public abstract class DrawNode : ScriptableObject
    {
        /// <summary>
        /// Draws the node's window.
        /// </summary>
        /// <param name="baseNode"></param>
        public abstract void DrawWindow(BaseNode baseNode);

        /// <summary>
        /// Draws the node's curve.
        /// </summary>
        /// <param name="baseNode"></param>
        public abstract void DrawCurve(BaseNode baseNode);
    }
}