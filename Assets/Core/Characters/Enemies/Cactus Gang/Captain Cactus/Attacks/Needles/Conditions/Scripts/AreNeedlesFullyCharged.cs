// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the needles are fully charged.
    /// </summary>
    [CreateAssetMenu(fileName = "Are Needles Fully Charged", menuName = "Behavior Tree Pattern/New Condition/Specific/Cactus/Are Needles Fully Charged")]
    public class AreNeedlesFullyCharged : Condition
    {
        private void OnValidate()
        {
            Description = "Are the needles fully charged?";
        }

        public override bool CheckCondition(StateManager state)
        {
            Needles attack = state as Needles;
            return attack.AreNeedlesFullyCharged;
        }
    }
}
