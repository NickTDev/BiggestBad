// Merle Roji

using System.Collections.Generic;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Contains behavior that can only be performed while in a certain State.
    /// </summary>
    [CreateAssetMenu(fileName = "State", menuName = "Behavior Tree Pattern/New State", order = 1)]
    public class State : ScriptableObject
    {
        public StateActions[] OnFixedUpdate;
        public StateActions[] OnUpdate;
        public StateActions[] OnEnter;
        public StateActions[] OnExit;

        public int IDCount;

        public List<Transition> Transitions = new List<Transition>();

        /// <summary>
        /// Actions performed On Enter.
        /// </summary>
        public void OnEnterExecute(StateManager states)
        {
            ExecuteActions(states, OnEnter);
        }

        /// <summary>
        /// Actions performed in the On FixedUpdate list in the FixedUpdate loop.
        /// </summary>
        /// <param name="states"></param>
        public void FixedTick(StateManager states)
        {
            ExecuteActions(states, OnFixedUpdate);
        }

        /// <summary>
        /// Actions performed in the On Update list in the Update loop.
        /// </summary>
        public void Tick(StateManager states)
        {
            ExecuteActions(states, OnUpdate);
            CheckTransitions(states);
        }

        /// <summary>
        /// Actions performed On Exit.
        /// </summary>
        public void OnExitExecute(StateManager states)
        {
            ExecuteActions(states, OnExit);
        }

        /// <summary>
        /// Checks if Transitions have been passed or not.
        /// </summary>
        /// <param name="states"></param>
        public void CheckTransitions(StateManager states)
        {
            for (int i = 0; i < Transitions.Count; i++)
            {
                if (Transitions[i].Disable) continue;
                if (Transitions[i].ConditionToPass.CheckCondition(states))
                {
                    if (Transitions[i].TargetState != null)
                    {
                        // handles switching states
                        states.CurrentState = Transitions[i].TargetState;
                        OnExitExecute(states);
                        states.CurrentState.OnEnterExecute(states);
                    }

                    return;
                }
            }
        }
        
        /// <summary>
        /// Executes the actions in the specific state actions list.
        /// </summary>
        /// <param name="states"></param>
        public void ExecuteActions(StateManager states, StateActions[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null) list[i].Execute(states);
            }
        }

        /// <summary>
        /// Returns a new transition and adds it to the list of transitions.
        /// </summary>
        /// <returns></returns>
        public Transition AddTransition()
        {
            Transition returnValue = new Transition();
            Transitions.Add(returnValue);
            returnValue.ID = IDCount;
            IDCount++;

            return returnValue;
        }

        /// <summary>
        /// Returns a Transition with a given ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Transition GetTransition(int ID)
        {
            for (int i = 0; i < Transitions.Count; i++)
            {
                if (Transitions[i].ID == ID) return Transitions[i];
            }

            return null;
        }

        /// <summary>
        /// Removes a transition from the transitions list at a given ID.
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveTransition(int ID)
        {
            Transition transition = GetTransition(ID);
            if (transition != null)
            {
                Transitions.Remove(transition);
            }
        }
    }
}
