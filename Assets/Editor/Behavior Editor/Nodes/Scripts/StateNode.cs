// Merle Roji

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// A Node that contains a State.
    /// </summary>
    [CreateAssetMenu(fileName = "State Node", menuName = "Behavior Editor/Nodes/New State Node", order = 0)]
    public class StateNode : DrawNode
    {
        private const int BASE_HEIGHT = 300;

        public override void DrawWindow(BaseNode baseNode)
        {
            if (baseNode.StateRef.CurrentState == null)
            {
                EditorGUILayout.LabelField("Add State to Modify: ");
            }
            else
            {
                if (!baseNode.Collapse)
                {
                    
                }
                else
                {
                    baseNode.WindowRect.height = (int)(BASE_HEIGHT * 0.25f);
                }

                baseNode.Collapse = EditorGUILayout.Toggle(" ", baseNode.Collapse);
            }

            baseNode.StateRef.CurrentState = (State)EditorGUILayout.ObjectField(baseNode.StateRef.CurrentState, typeof(State), false);

            if (baseNode.PreviousCollapse != baseNode.Collapse)
            {
                baseNode.PreviousCollapse = baseNode.Collapse;
            }

            if (baseNode.StateRef.PreviousState != baseNode.StateRef.CurrentState)
            {
                // logic that checks if you changed the state node
                // no need to serialize state on every update loop
                baseNode.IsDuplicate = BehaviorEditor.s_settings.CurrentGraph.IsStateDuplicate(baseNode);
                baseNode.StateRef.PreviousState = baseNode.StateRef.CurrentState;

                if (!baseNode.IsDuplicate)
                {
                    Vector3 position = new Vector3(baseNode.WindowRect.x, baseNode.WindowRect.y, 0);
                    position.x += baseNode.WindowRect.width * 2;

                    SetUpReorderableLists(baseNode);

                    // Load Transitions
                    for (int i = 0; i < baseNode.StateRef.CurrentState.Transitions.Count; i++)
                    {
                        position.y += i * (int)(BASE_HEIGHT * 0.25f);
                        BehaviorEditor.AddTransitionNodeFromTransition(baseNode.StateRef.CurrentState.Transitions[i], baseNode, position);
                    }

                    BehaviorEditor.s_forceSetDirty = true;
                }
            }

            if (baseNode.IsDuplicate)
            {
                EditorGUILayout.LabelField("State is a duplicate!!");
                baseNode.WindowRect.height = (int)(BASE_HEIGHT * 0.25f);

                return;
            }

            if (baseNode.StateRef.CurrentState != null)
            {
                baseNode.IsAssigned = true;

                if (!baseNode.Collapse)
                {
                    if (baseNode.StateRef.SerializedState == null)
                    {
                        SetUpReorderableLists(baseNode);
                    }

                    int standardHeight = BASE_HEIGHT;
                    baseNode.StateRef.SerializedState.Update();

                    baseNode.ShowActions = EditorGUILayout.Toggle("Show Actions ", baseNode.ShowActions);
                    if (baseNode.ShowActions)
                    {
                        EditorGUILayout.LabelField("");
                        baseNode.StateRef.OnFixedUpdateList.DoLayoutList();
                        EditorGUILayout.LabelField("");
                        baseNode.StateRef.OnUpdateList.DoLayoutList();

                        standardHeight += (int)
                        (
                            (BASE_HEIGHT * 0.3f) + (baseNode.StateRef.OnFixedUpdateList.count + baseNode.StateRef.OnUpdateList.count) * (BASE_HEIGHT * 0.075f)
                        );
                    }

                    baseNode.ShowEnterExit = EditorGUILayout.Toggle("Show Enter/Exit ", baseNode.ShowEnterExit);
                    if (baseNode.ShowEnterExit)
                    {
                        EditorGUILayout.LabelField("");
                        baseNode.StateRef.OnEnterList.DoLayoutList();
                        EditorGUILayout.LabelField("");
                        baseNode.StateRef.OnExitList.DoLayoutList();

                        standardHeight += (int)
                        (
                            (BASE_HEIGHT * 0.3f) + (baseNode.StateRef.OnEnterList.count + baseNode.StateRef.OnExitList.count) * (BASE_HEIGHT * 0.075f)
                        );
                    }

                    if (!baseNode.ShowActions && !baseNode.ShowEnterExit)
                    {
                        standardHeight = (int)(BASE_HEIGHT * 0.4f);
                    }
                    else if ((!baseNode.ShowActions && baseNode.ShowEnterExit) || (baseNode.ShowActions && !baseNode.ShowEnterExit))
                    {
                        standardHeight -= (int)(BASE_HEIGHT * 0.2f);
                    }
                    else
                    {
                        standardHeight -= (int)(BASE_HEIGHT * 0.025f);
                    }

                    baseNode.StateRef.SerializedState.ApplyModifiedProperties();
                    baseNode.WindowRect.height = standardHeight;
                }
            }
            else
            {
                baseNode.IsAssigned = false;
            }
        }

        private void SetUpReorderableLists(BaseNode baseNode)
        {
            baseNode.StateRef.SerializedState = new SerializedObject(baseNode.StateRef.CurrentState);

            baseNode.StateRef.OnFixedUpdateList = new ReorderableList(baseNode.StateRef.SerializedState,
                baseNode.StateRef.SerializedState.FindProperty("OnFixedUpdate"), true, true, true, true);
            baseNode.StateRef.OnUpdateList = new ReorderableList(baseNode.StateRef.SerializedState,
                baseNode.StateRef.SerializedState.FindProperty("OnUpdate"), true, true, true, true);
            baseNode.StateRef.OnEnterList = new ReorderableList(baseNode.StateRef.SerializedState,
                baseNode.StateRef.SerializedState.FindProperty("OnEnter"), true, true, true, true);
            baseNode.StateRef.OnExitList = new ReorderableList(baseNode.StateRef.SerializedState,
                baseNode.StateRef.SerializedState.FindProperty("OnExit"), true, true, true, true);

            HandleReordableList(baseNode.StateRef.OnFixedUpdateList, "On Fixed Update"); // list of actions in the On Update list
            HandleReordableList(baseNode.StateRef.OnUpdateList, "On Update"); // list of actions in the On Update list
            HandleReordableList(baseNode.StateRef.OnEnterList, "On Enter"); // list of actions in the On Enter list
            HandleReordableList(baseNode.StateRef.OnExitList, "On Exit"); // list of actions in the On Exit list
        }

        /// <summary>
        /// Allows the lists of State Actions to be visible.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="targetName"></param>
        private void HandleReordableList(ReorderableList list, string targetName)
        {
            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, targetName);
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
        }

        public override void DrawCurve(BaseNode baseNode)
        {
            
        }

        /// <summary>
        /// Returns a new transition from the current state.
        /// </summary>
        /// <returns></returns>
        public Transition AddTransition(BaseNode baseNode)
        {
            return baseNode.StateRef.CurrentState.AddTransition();
        }

        public void ClearReferences()
        {
            //BehaviorEditor.ClearWindowsFromList(Dependencies);
            //Dependencies.Clear();
        }
    }
}