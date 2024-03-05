// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Shows the Skill menu.
    /// </summary>
    [CreateAssetMenu(fileName = "Show Skill Menu", menuName = "Behavior Tree Pattern/New Action/Common/Menu/Show Skill Menu")]
    public class ShowSkillMenu : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController character = (PlayerBattleController)states;
            character.TurnManager.ToggleSkillMenu(true);
        }
    }
}