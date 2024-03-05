// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player failed the entire attack.
    /// </summary>
    [CreateAssetMenu(fileName = "Complete Corrosive Failure", menuName = "Behavior Tree Pattern/New Condition/Specific/Angel Goode/Complete Corrosive Failure")]
    public class CompleteCorrosiveFailure : Condition
    {
        private void OnValidate()
        {
            Description = "Did the player fail the entire attack?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CorrosiveBomb attack = (CorrosiveBomb)state;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            if (attack.SliderIndex >= attack.AmountOfSliders - 1)
            {
                if (attack.SuccessCount <= 0 && (player.Player1ActionPressed || attack.CurrentTime <= 0)) // check if any successes were made. 
                {
                    return true;
                }
            }

            return false;
        }
    }
}