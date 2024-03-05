// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the corrosive index has gone above the amount of sliders.
    /// </summary>
    [CreateAssetMenu(fileName = "Is C Index Passed", menuName = "Behavior Tree Pattern/New Condition/Specific/Angel Goode/Is C Index Passed")]
    public class IsCorrosiveIndexPassed : Condition
    {
        private void OnValidate()
        {
            Description = "Is the index above the amount of sliders?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CorrosiveBomb attack = (CorrosiveBomb)state;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            
            if (attack.SliderIndex >= attack.AmountOfSliders - 1 && attack.SuccessCount > 0) // check if the index has reached the amount of sliders - 1.
            {
                if (player.Player1ActionPressed) 
                {
                    return true;
                }
            }

            return false;
        }
    }
}