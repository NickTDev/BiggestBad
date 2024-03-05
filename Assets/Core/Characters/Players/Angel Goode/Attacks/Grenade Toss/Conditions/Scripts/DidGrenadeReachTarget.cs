// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the grenade reached the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Grenade Reach Target", menuName = "Behavior Tree Pattern/New Condition/Specific/Angel Goode/Did Grenade Reach Target")]
    public class DidGrenadeReachTarget : Condition
    {
        private void OnValidate()
        {
            Description = "Did the grenade reach the target?";
        }

        public override bool CheckCondition(StateManager state)
        {
            GrenadeToss attack = (GrenadeToss)state;
            return attack.DidGrenadeReachTarget;
        }
    }
}