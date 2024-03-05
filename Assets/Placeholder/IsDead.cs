// Merle Roji

using UnityEngine;

namespace T02
{
    [CreateAssetMenu(fileName = "Is Dead", menuName = "Behavior Tree Pattern/New Condition/Placeholder/Is Dead")]
    public class IsDead : Condition
    {
        private void OnEnable()
        {
            Description = "Is Health 0 or less?";
        }

        public override bool CheckCondition(StateManager stateMachine)
        {
            //TestStateManager testMachine = (TestStateManager)stateMachine;

            return false; //testMachine.Player.Health <= 0;
        }
    }
}
