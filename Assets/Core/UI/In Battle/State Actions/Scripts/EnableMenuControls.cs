// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Enables the menu controls.
    /// </summary>
    [CreateAssetMenu(fileName = "Enable Menu Controls", menuName = "Behavior Tree Pattern/New Action/Common/Menu/Enable Menu Controls")]
    public class EnableMenuControls : StateActions
    {
        public override void Execute(StateManager states)
        {
            if (states is PlayerBattleController)
            {
                PlayerBattleController player = (PlayerBattleController)states;
                player.EnableMenuControls();
            }
            // else if states is PlayerOverworldController
        }
    }
}
