// Merle Roji

using UnityEngine;

namespace T02
{
    /// <summary>
    /// A Condition states the terms a state must pass before transitioning to another state.
    /// </summary>
    public abstract class Condition : ScriptableObject
    {
        public string Description;

        /// <summary>
        /// Checks the condition that must be passed.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public abstract bool CheckCondition(StateManager state);
    }
}

