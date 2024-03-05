// Merle Roji

using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// Parent class for all node structures.
    /// </summary>
    [Serializable]
    public class BaseNode
    {
        public int ID;
        public DrawNode NodeToDraw;
        public Rect WindowRect;
        public string WindowTitle;
        public int EnterNode;
        public int TargetNode;
        public bool IsDuplicate;
        public bool IsAssigned;
        public bool ShowActions = true;
        public bool ShowEnterExit = false;
        public string Comment;

        public bool Collapse;
        [HideInInspector] public bool PreviousCollapse;

        [SerializeField] public StateNodeReferences StateRef;
        [SerializeField] public TransitionNodeReferences TransitionRef;

        /// <summary>
        /// Draws the window containing the Node's information.
        /// </summary>
        public void DrawWindow()
        {
            if (NodeToDraw != null)
            {
                NodeToDraw.DrawWindow(this);
            }
        }

        /// <summary>
        /// Draws curved connections from window to window.
        /// </summary>
        public void DrawCurve()
        {
            if (NodeToDraw != null)
            {
                NodeToDraw.DrawCurve(this);
            }
        }
    }
}