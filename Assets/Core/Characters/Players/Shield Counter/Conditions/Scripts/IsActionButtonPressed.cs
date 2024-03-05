// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player pressed their respective action button.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Action Button Pressed", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Is Action Button Pressed")]
    public class IsActionButtonPressed : Condition
    {
        private void OnValidate()
        {
            Description = "Is the action button pressed?";
        }

        public override bool CheckCondition(StateManager state)
        {
            ShieldCounter attack = (ShieldCounter)state;
            return attack.PressedActionButton;
        }
    }
}
