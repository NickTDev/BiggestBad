// Merle Roji

using System;
using UnityEngine;

namespace T02.BehaviorEditor
{
    /// <summary>
    /// Stores the information needed to load onto the transition node.
    /// </summary>
    [Serializable]
    public class TransitionNodeReferences
    {
        [HideInInspector] public Condition PreviousCondition;
        public int TransitionID;
    }
}

