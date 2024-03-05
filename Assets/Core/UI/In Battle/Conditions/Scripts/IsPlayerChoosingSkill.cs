// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player is choosing a skill.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Player Choosing Skill", menuName = "Behavior Tree Pattern/New Condition/Common/Menu/Is Choosing Skill")]
    public class IsPlayerChoosingSkill : Condition
    {
        private void OnValidate()
        {
            Description = "Is the player choosing a skill?";
        }

        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            return player.TurnManager.PressedSkillOptionInMenu;
        }
    }
}
