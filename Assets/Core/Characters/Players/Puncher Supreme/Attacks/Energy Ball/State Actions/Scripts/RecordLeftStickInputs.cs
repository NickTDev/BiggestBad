// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Records Left Stick inputs.
    /// </summary>
    [CreateAssetMenu(fileName = "Record Left Stick Inputs", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Record Left Stick Inputs")]
    public class RecordLeftStickInputs : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnergyBall attack = (EnergyBall)states;
            attack.RecordMovements();
        }
    }
}