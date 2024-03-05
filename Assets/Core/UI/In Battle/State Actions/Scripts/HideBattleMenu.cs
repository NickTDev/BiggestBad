// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Hides the battle menu.
    /// </summary>
    [CreateAssetMenu(fileName = "Hide Battle Menu", menuName = "Behavior Tree Pattern/New Action/Common/Menu/Hide Battle Menu")]
    public class HideBattleMenu : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController character = (PlayerBattleController)states;
            character.TurnManager.ToggleBattleMenu(false);
        }
    }
}