// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Hides the Skill menu.
    /// </summary>
    [CreateAssetMenu(fileName = "Hide Skill Menu", menuName = "Behavior Tree Pattern/New Action/Common/Menu/Hide Skill Menu")]
    public class HideSkillMenu : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController character = (PlayerBattleController)states;
            character.TurnManager.ToggleSkillMenu(false);
        }
    }
}