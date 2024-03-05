// Merle Roji

using UnityEngine;

namespace T02
{
    /// <summary>
    /// List of actions performed while an entity has entered a state.
    /// </summary>
    public abstract class StateActions : ScriptableObject
    {
        /// <summary>
        /// Executes the actions.
        /// </summary>
        /// <param name="states"></param>
        public abstract void Execute(StateManager states);
    }
}
