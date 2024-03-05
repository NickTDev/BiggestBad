// Merle Roji

using UnityEngine;

namespace T02
{
    [CreateAssetMenu(fileName = "Add Health", menuName = "Behavior Tree Pattern/New Action/Test/Add Health")]
    public class ChangeHealth : StateActions
    {
        public override void Execute(StateManager stateMachine)
        {
            //TestStateManager testMachine = (TestStateManager)stateMachine;

            //testMachine.Player.Health += 10;
        }
    }
}
