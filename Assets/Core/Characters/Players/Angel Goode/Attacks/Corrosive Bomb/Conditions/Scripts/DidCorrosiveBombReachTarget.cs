// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the corrosive bomb reached the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Corrosive Bomb Reach Target", menuName = "Behavior Tree Pattern/New Condition/Specific/Angel Goode/Did Corrosive Bomb Reach Target")]
    public class DidCorrosiveBombReachTarget : Condition
    {
        private void OnValidate()
        {
            Description = "Did the bomb reach the target?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CorrosiveBomb attack = (CorrosiveBomb)state;
            return attack.DidBombReachTarget;
        }
    }
}