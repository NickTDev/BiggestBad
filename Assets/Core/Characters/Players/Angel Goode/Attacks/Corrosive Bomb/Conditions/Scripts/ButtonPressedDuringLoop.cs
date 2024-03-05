// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player pressed the button during the slider loop.
    /// </summary>
    [CreateAssetMenu(fileName = "Button Pressed During Loop", menuName = "Behavior Tree Pattern/New Condition/Specific/Angel Goode/Button Pressed During Loop")]
    public class ButtonPressedDuringLoop : Condition
    {
        private void OnValidate()
        {
            Description = "Did the player press the button?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CorrosiveBomb attack = (CorrosiveBomb)state;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            if (attack.SliderIndex < attack.AmountOfSliders - 1)
            {
                if (player.Player1ActionPressed) // check if any successes were made. 
                {
                    return true;
                }
            }

            return false;
        }
    }
}