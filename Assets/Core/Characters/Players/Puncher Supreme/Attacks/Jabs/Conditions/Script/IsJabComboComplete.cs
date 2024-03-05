// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Check if the jab combo is complete.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Jab Combo Complete", menuName = "Behavior Tree Pattern/New Condition/Specific/Puncher Supreme/Is Jab Combo Complete")]
    public class IsJabComboComplete : Condition
    {
        private void OnValidate()
        {
            Description = "Is the Jab Combo Complete?";
        }

        public override bool CheckCondition(StateManager state)
        {
            Jabs attack = (Jabs)state;
            return attack.IsComboComplete;
        }
    }
}