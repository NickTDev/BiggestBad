// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the goo bomb reached the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Goo Bomb Reach Target", menuName = "Behavior Tree Pattern/New Condition/Specific/Angel Goode/Did Goo Bomb Reach Target")]
    public class DidGooBombReachTarget : Condition
    {
        private void OnValidate()
        {
            Description = "Did the bomb reach the target?";
        }

        public override bool CheckCondition(StateManager state)
        {
            GooBomb attack = (GooBomb)state;
            return attack.DidBombReachTarget;
        }
    }
}