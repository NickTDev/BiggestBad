// Merle Roji

using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// Stores the information needed to load onto the state node.
    /// </summary>
    [Serializable]
    public class StateNodeReferences
    {
        [HideInInspector] public State CurrentState;
        [HideInInspector] public State PreviousState;

        // we must serialize states to access them via inspector
        [HideInInspector] public SerializedObject SerializedState;
        [HideInInspector] public ReorderableList OnFixedUpdateList; // lists the actions performed while in the fixed update loop
        [HideInInspector] public ReorderableList OnUpdateList; // lists the actions performed while in the update loop
        [HideInInspector] public ReorderableList OnEnterList; // lists the actions performed right when entering the state
        [HideInInspector] public ReorderableList OnExitList; // lists the actions performed right when exiting the state
    }
}
