// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if down was pressed.
    /// </summary>
    [CreateAssetMenu(fileName = "Check If Down Pressed", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Check If Down Pressed")]
    public class CheckIfDownPressed : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnergyBall attack = (EnergyBall)states;
            attack.CheckIfDownPressed();
        }
    }
}