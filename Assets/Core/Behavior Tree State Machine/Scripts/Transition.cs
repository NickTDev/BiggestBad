// Merle Roji

using System;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Transition makes a state transition between other states.
    /// </summary>
    [Serializable]
    public class Transition
    {
        public int ID;
        public Condition ConditionToPass;
        public State TargetState;
        public bool Disable;
    }
}
