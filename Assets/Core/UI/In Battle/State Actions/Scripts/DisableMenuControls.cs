// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Disable the menu controls.
    /// </summary>
    [CreateAssetMenu(fileName = "Disable Menu Controls", menuName = "Behavior Tree Pattern/New Action/Common/Menu/Disable Menu Controls")]
    public class DisableMenuControls : StateActions
    {
        public override void Execute(StateManager states)
        {
            if (states is PlayerBattleController)
            {
                PlayerBattleController player = (PlayerBattleController)states;
                player.DisableMenuControls();
            }
            // else if states is PlayerOverworldController
        }
    }
}
