// Merle Roji

using T02.Characters.InBattle;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Checks input from the player for the goo slider.
    /// </summary>
    [CreateAssetMenu(fileName = "Goo Slider Input", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Goo Slider Input")]
    public class GooSliderInput : StateActions
    {
        public override void Execute(StateManager states)
        {
            GooBomb attack = (GooBomb)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            // button press
            if (player.Player1ActionHeld)
            {
                attack.ButtonHeld = true;
            }
            else
            {
                attack.ButtonHeld = false;
            }
        }
    }
}