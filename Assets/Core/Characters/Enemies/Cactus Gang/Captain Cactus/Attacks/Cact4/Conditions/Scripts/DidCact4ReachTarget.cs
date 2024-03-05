// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the cact4 bomb reached the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Cact4 Reach Target", menuName = "Behavior Tree Pattern/New Condition/Specific/Cactus/Did Cact4 Reach Target")]
    public class DidCact4ReachTarget : Condition
    {
        private void OnValidate()
        {
            Description = "Did the bomb reach the target?";
        }

        public override bool CheckCondition(StateManager state)
        {
            Cact4Attack attack = (Cact4Attack)state;
            return attack.DidBombReachTarget;
        }
    }
}