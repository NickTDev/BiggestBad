// Merle Roji

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// The editor for behavior trees.
    /// </summary>
    public class BehaviorEditor : EditorWindow
    {
        #region VARIABLES

        private Vector3 _mousePosition;
        private bool _clickedOnWindow;
        private BaseNode _selectedNode;
        private int _selectedIndex;
        private const int BASE_WINDOW_WIDTH = 175;

        public static BehaviorEditorSettings s_settings;
        private int _transitionFromID;
        private Rect _mouseRect = new Rect(0, 0, 1, 1);
        //private GUIStyle _activeStyle;

        public static StateManager s_currentStateManager;
        public static bool s_forceSetDirty;
        private static StateManager s_previousStateManager;
        private static State s_previousState;

        public enum UserAction
        {
            AddState,
            AddTransitionNode,
            CommentNode,
            DeleteNode,
            ConnectTransition,
            MakePortal,
        }

        #endregion

        #region INIT

        /// <summary>
        /// Creates the window for the editor.
        /// </summary>
        [MenuItem("Behavior Editor/Editor")]
        private static void ShowEditor()
        {
            BehaviorEditor editor = EditorWindow.GetWindow<BehaviorEditor>();
            editor.minSize = new Vector2(1280, 720); // 720p
        }

        private void OnEnable()
        {
            s_settings = Resources.Load("BehaviorEditorSettings") as BehaviorEditorSettings;
            //_activeStyle = s_settings.ActiveSkin.GetStyle("selectWindow");
        }

        private void Update()
        {
            if (s_currentStateManager != null)
            {
                if (s_previousState != s_currentStateManager.CurrentState)
                {
                    Repaint();
                    s_previousState = s_currentStateManager.CurrentState;
                }
            }
        }

        #endregion

        #region GUI METHODS

        private void OnGUI()
        {
            if (Selection.activeTransform != null)
            {
                s_currentStateManager = Selection.activeTransform.GetComponentInChildren<StateManager>();
                if (s_previousStateManager != s_currentStateManager)
                {
                    s_previousStateManager = s_currentStateManager;
                    Repaint();
                }
            }

            Event e = Event.current;
            _mousePosition = e.mousePosition;
            CheckInput(e);
            DrawWindows();

            if (GUI.changed)
            {
                s_settings.CurrentGraph.DeleteWindowsThatNeedTo();
                Repaint();
            }

            if (s_settings.ConnectTransition)
            {
                _mouseRect.x = _mousePosition.x;
                _mouseRect.y = _mousePosition.y;
                Rect from = s_settings.CurrentGraph.GetNodeWithIndex(_transitionFromID).WindowRect;
                DrawNodeCurve(from, _mouseRect, true, Color.magenta);
                Repaint();
            }

            if (s_forceSetDirty)
            {
                s_forceSetDirty = false;
                EditorUtility.SetDirty(s_settings);
                EditorUtility.SetDirty(s_settings.CurrentGraph);

                for (int i = 0; i < s_settings.CurrentGraph.AllWindows.Count; i++)
                {
                    BaseNode node = s_settings.CurrentGraph.AllWindows[i];
                    if (node.StateRef.CurrentState != null)
                    {
                        EditorUtility.SetDirty(node.StateRef.CurrentState);
                    }
                }
            }
        }

        private void OnInspectorUpdate()
        {
            s_settings.CurrentGraph.DeleteWindowsThatNeedTo();
            Repaint();
        }

        /// <summary>
        /// Draws all of the node and curve windows.
        /// </summary>
        private void DrawWindows()
        {
            BeginWindows();
            EditorGUILayout.LabelField(" ", GUILayout.Width(BASE_WINDOW_WIDTH));
            EditorGUILayout.LabelField("Assign Graph: ", GUILayout.Width(BASE_WINDOW_WIDTH));
            s_settings.CurrentGraph = (BehaviorGraph)EditorGUILayout.ObjectField(s_settings.CurrentGraph, typeof(BehaviorGraph), false, GUILayout.Width(BASE_WINDOW_WIDTH));

            if (s_settings.CurrentGraph != null)
            {
                foreach (BaseNode node in s_settings.CurrentGraph.AllWindows) // draw all the curves
                {
                    node.DrawCurve();
                }

                for (int i = 0; i < s_settings.CurrentGraph.AllWindows.Count; i++) // draw all of the node windows
                {
                    BaseNode baseNode = s_settings.CurrentGraph.AllWindows[i];

                    if (baseNode.NodeToDraw is StateNode)
                    {
                        if (s_currentStateManager != null && baseNode.StateRef.CurrentState == s_currentStateManager.CurrentState)
                        {
                            baseNode.WindowRect = GUI.Window(i, baseNode.WindowRect, DrawNodeWindow, baseNode.WindowTitle/*, _activeStyle*/);
                        }
                        else
                        {
                            baseNode.WindowRect = GUI.Window(i, baseNode.WindowRect, DrawNodeWindow, baseNode.WindowTitle);
                        }
                    }
                    else
                    {
                        baseNode.WindowRect = GUI.Window(i, baseNode.WindowRect, DrawNodeWindow, baseNode.WindowTitle);
                    }
                }
            }

            EndWindows();
        }

        /// <summary>
        /// Draws the nodes into the window and allows them to be dragged around.
        /// </summary>
        /// <param name="id"></param>
        private void DrawNodeWindow(int id)
        {
            s_settings.CurrentGraph.AllWindows[id].DrawWindow();
            GUI.DragWindow();
        }

        #endregion

        #region INPUT METHODS

        /// <summary>
        /// Checks input from the user.
        /// </summary>
        private void CheckInput(Event e)
        {
            if (s_settings.CurrentGraph == null) return;

            // right click
            if (e.button == 1 && !s_settings.ConnectTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }

            // left click
            if (e.button == 0 && !s_settings.ConnectTransition)
            {
                if (e.type == EventType.MouseDown)
                {

                }
            }

            // connecting transitions
            if (e.button == 0 && s_settings.ConnectTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    ConnectTransition();
                }
            }
        }

        /// <summary>
        /// Creates a menu with options when right clicking on the behavior editor.
        /// </summary>
        /// <param name="e"></param>
        private void RightClick(Event e)
        {
            _selectedIndex = -1;
            _clickedOnWindow = false;
            for (int i = 0; i < s_settings.CurrentGraph.AllWindows.Count; i++)
            {
                if (s_settings.CurrentGraph.AllWindows[i].WindowRect.Contains(e.mousePosition))
                {
                    _clickedOnWindow = true;
                    _selectedNode = s_settings.CurrentGraph.AllWindows[i];
                    _selectedIndex = i;
                    break;
                }
            }

            if (!_clickedOnWindow)
            {
                AddNewNode(e);
            }
            else
            {
                ModifyNode(e);
            }
        }

        /// <summary>
        /// Connects a state to another with a condition in between.
        /// </summary>
        private void ConnectTransition()
        {
            s_settings.ConnectTransition = false;
            _clickedOnWindow = false;

            for (int i = 0; i < s_settings.CurrentGraph.AllWindows.Count; i++)
            {
                if (s_settings.CurrentGraph.AllWindows[i].WindowRect.Contains(_mousePosition))
                {
                    _clickedOnWindow = true;
                    _selectedNode = s_settings.CurrentGraph.AllWindows[i];
                    _selectedIndex = i;
                    break;
                }
            }

            if (_clickedOnWindow)
            {
                if (_selectedNode.NodeToDraw is StateNode || _selectedNode.NodeToDraw is PortalNode)
                {
                    if (_selectedNode.ID != _transitionFromID)
                    {
                        BaseNode transitionNode = s_settings.CurrentGraph.GetNodeWithIndex(_transitionFromID);
                        transitionNode.TargetNode = _selectedNode.ID;

                        BaseNode enterNode = s_settings.CurrentGraph.GetNodeWithIndex(transitionNode.EnterNode);
                        Transition transition = enterNode.StateRef.CurrentState.GetTransition(transitionNode.TransitionRef.TransitionID);

                        transition.TargetState = _selectedNode.StateRef.CurrentState;
                    }
                }
            }
        }

        #endregion

        #region CONTEXT MENUS

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="e"></param>
        private void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();

            // adds options to the dropdown menu
            menu.AddSeparator("");
            if (s_settings.CurrentGraph != null)
            {
                menu.AddItem(new GUIContent("Add State"), false, ContextCallback, UserAction.AddState);
                menu.AddItem(new GUIContent("Add Portal"), false, ContextCallback, UserAction.MakePortal);
                menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserAction.CommentNode);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Add State"));
                menu.AddDisabledItem(new GUIContent("Add Comment"));
            }

            menu.ShowAsContext();
            e.Use();
        }

        /// <summary>
        /// Modifies a given Node.
        /// </summary>
        /// <param name="e"></param>
        private void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();

            if (_selectedNode.NodeToDraw is StateNode)
            {
                if (_selectedNode.StateRef.CurrentState != null)
                {
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Add Condition"), false, ContextCallback, UserAction.AddTransitionNode);
                }
                else
                {
                    menu.AddSeparator("");
                    menu.AddDisabledItem(new GUIContent("Add Condition"));
                }

                
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, UserAction.DeleteNode);
            }

            if (_selectedNode.NodeToDraw is PortalNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, UserAction.DeleteNode);
            }

            if (_selectedNode.NodeToDraw is TransitionNode)
            {
                if (_selectedNode.IsDuplicate || !_selectedNode.IsAssigned)
                {
                    menu.AddSeparator("");
                    menu.AddDisabledItem(new GUIContent("Make Transition"));
                }
                else
                {
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, UserAction.ConnectTransition);
                }

                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, UserAction.DeleteNode);
            }

            if (_selectedNode.NodeToDraw is CommentNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, UserAction.DeleteNode);
            }

            menu.ShowAsContext();
            e.Use();
        }

        /// <summary>
        /// Menu items for when clicking inside or outside of a window.
        /// </summary>
        /// <param name="o"></param>
        private void ContextCallback(object o)
        {
            UserAction action = (UserAction)o;

            switch (action)
            {
                case UserAction.AddState:
                    {
                        s_settings.AddNodeOnGraph(s_settings.StateNodeLogic, BASE_WINDOW_WIDTH, BASE_WINDOW_WIDTH * 0.5f, "State", _mousePosition);

                        break;
                    }
                case UserAction.MakePortal:
                    {
                        s_settings.AddNodeOnGraph(s_settings.PortalNodeLogic, BASE_WINDOW_WIDTH, BASE_WINDOW_WIDTH * 0.25f, "Portal", _mousePosition);

                        break;
                    }
                case UserAction.AddTransitionNode:
                    {
                        AddTransitionNode(_selectedNode, _mousePosition);

                        break;
                    }
                case UserAction.CommentNode:
                    {
                        BaseNode commentNode =
                            s_settings.AddNodeOnGraph(s_settings.CommentNodeLogic, BASE_WINDOW_WIDTH, BASE_WINDOW_WIDTH * 0.5f, "Comment", _mousePosition);
                        commentNode.Comment = "This is a comment.";

                        break;
                    }
                case UserAction.DeleteNode:
                    {
                        if (_selectedNode.NodeToDraw is TransitionNode)
                        {
                            BaseNode enterNode = s_settings.CurrentGraph.GetNodeWithIndex(_selectedNode.EnterNode);
                            enterNode.StateRef.CurrentState.RemoveTransition(_selectedNode.TransitionRef.TransitionID);
                        }

                        s_settings.CurrentGraph.DeleteNode(_selectedNode.ID);

                        break;
                    }
                case UserAction.ConnectTransition:
                    {
                        _transitionFromID = _selectedNode.ID;
                        s_settings.ConnectTransition = true;

                        break;
                    }
            }

            s_forceSetDirty = true;
        }

        /// <summary>
        /// Creates and Adds a transition node to the editor.
        /// </summary>
        /// <param name="enterNode"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static BaseNode AddTransitionNode(BaseNode enterNode, Vector3 position)
        {
            BaseNode transitionNode = s_settings.AddNodeOnGraph(s_settings.TransitionNodeLogic, BASE_WINDOW_WIDTH, BASE_WINDOW_WIDTH * 0.5f, "Condition", position);
            transitionNode.EnterNode = enterNode.ID;
            Transition transition = s_settings.StateNodeLogic.AddTransition(enterNode);
            transitionNode.TransitionRef.TransitionID = transition.ID;

            return transitionNode;
        }

        /// <summary>
        /// Creates and Adds a transition node to the editor from another transition.
        /// </summary>
        /// <param name="transition"></param>
        /// <param name="enterNode"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static BaseNode AddTransitionNodeFromTransition(Transition transition, BaseNode enterNode, Vector3 position)
        {
            BaseNode transitionNode = s_settings.AddNodeOnGraph(s_settings.TransitionNodeLogic, BASE_WINDOW_WIDTH, BASE_WINDOW_WIDTH * 0.5f, "Condition", position);
            transitionNode.EnterNode = enterNode.ID;
            transitionNode.TransitionRef.TransitionID = transition.ID;

            return transitionNode;
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// Draws the curves depicting the transitions between nodes.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="left"></param>
        /// <param name="curveColor"></param>
        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
        {
            Vector3 startPos = new Vector3(
                (left)?start.x + start.width : start.x, // X : if left, x + width, otherwise x
                start.y + (start.height * 0.5f),        // Y
                0                                       // Z
                );

            Vector3 endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;

            Color shadow = new Color(0, 0, 0, 0.15f);
            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadow, null, (i + 1) * 1);
            }

            Handles.DrawBezier(startPos, endPos, startTan, endTan, curveColor, null, 4);
        }

        /// <summary>
        /// Clears the base node windows.
        /// </summary>
        /// <param name="nodes"></param>
        public static void ClearWindowsFromList(List<BaseNode> nodes)
        {
            //for (int i = 0; i < nodes.Count;i++)
            //{
            //    //if (s_windows.Contains(nodes[i]))
            //    //{
            //    //    s_windows.Remove(nodes[i]);
            //    //}
            //}
        }

        #endregion
    }
}
