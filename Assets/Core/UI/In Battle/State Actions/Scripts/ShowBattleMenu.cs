// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Shows the battle menu.
    /// </summary>
    [CreateAssetMenu(fileName = "Show Battle Menu", menuName = "Behavior Tree Pattern/New Action/Common/Menu/Show Battle Menu")]
    public class ShowBattleMenu : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController character = (PlayerBattleController)states;
            character.TurnManager.ToggleBattleMenu(true);
        }
    }
}