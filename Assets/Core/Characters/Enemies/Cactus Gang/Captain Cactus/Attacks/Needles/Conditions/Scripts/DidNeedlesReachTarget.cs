// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the needles reached their target.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Needles Reach Target", menuName = "Behavior Tree Pattern/New Condition/Specific/Cactus/Did Needles Reach Target")]
    public class DidNeedlesReachTarget : Condition
    {
        private void OnValidate()
        {
            Description = "Did the needles hit?";
        }

        public override bool CheckCondition(StateManager state)
        {
            Needles attack = state as Needles;
            return attack.DidNeedlesReachTarget;
        }
    }
}
