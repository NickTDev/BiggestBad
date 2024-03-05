// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player chose a skill.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Player Choose Skill", menuName = "Behavior Tree Pattern/New Condition/Common/Menu/Did Player Choose Skill")]
    public class DidPlayerChooseSkill : Condition
    {
        private void OnValidate()
        {
            Description = "Did the player choose a skill?";
        }

        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            if (player.SkillList[player.TurnManager.SkillMenuSelection].Type == EnergyType.Spend)
            {
                if (player.Stats.CurrentEnergy < player.SkillList[player.TurnManager.SkillMenuSelection].BaseEnergyValue)
                {
                    //player.TurnManager.ToggleEnergyAlertPanel(true);
                    return false;
                }

                return player.TurnManager.PressedSelectSkill;
            }
            else
            {
                return player.TurnManager.PressedSelectSkill;
            }
        }
    }
}
