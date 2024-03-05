// Merle Roji

using UnityEngine;

namespace T02
{
    /// <summary>
    /// The State Manager acts as a parent class to all state machines
    /// </summary>
    public abstract class StateManager : MonoBehaviour
    {
        public State CurrentState;

        protected virtual void FixedUpdate()
        {
            FixedTickCurrentStates();
        }

        protected virtual void Update()
        {
            TickCurrentStates();
        }

        /// <summary>
        /// Fixed Updates a Current State's fixed tick loop.
        /// </summary>
        private void FixedTickCurrentStates()
        {
            if (CurrentState != null) CurrentState.FixedTick(this);
        }

        /// <summary>
        /// Updates a Current State's tick loop.
        /// </summary>
        private void TickCurrentStates()
        {
            if (CurrentState != null) CurrentState.Tick(this);
        }
    }
}