// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the grenade reached the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Energy Ball Reach Target", menuName = "Behavior Tree Pattern/New Condition/Specific/Puncher Supreme/Did Energy Ball Reach Target")]
    public class DidEnergyBallReachTarget : Condition
    {
        private void OnValidate()
        {
            Description = "Did the energy ball reach the target?";
        }

        public override bool CheckCondition(StateManager state)
        {
            EnergyBall attack = (EnergyBall)state;
            return attack.DidProjectileReachTarget;
        }
    }
}