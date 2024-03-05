// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the needles are destroyed.
    /// </summary>
    [CreateAssetMenu(fileName = "Are Needles Destroyed", menuName = "Behavior Tree Pattern/New Condition/Specific/Cactus/Are Needles Destroyed")]
    public class AreNeedlesDestroyed : Condition
    {
        private void OnValidate()
        {
            Description = "Are the needles destroyed?";
        }

        public override bool CheckCondition(StateManager state)
        {
            Needles attack = state as Needles;
            return attack.AreNeedlesDestroyed;
        }
    }
}
