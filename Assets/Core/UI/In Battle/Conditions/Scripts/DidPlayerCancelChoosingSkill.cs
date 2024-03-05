// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player is backing out of choosing a skill.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Player Cancel Choosing Skill", menuName = "Behavior Tree Pattern/New Condition/Common/Menu/Did Player Cancel Choosing Skill")]
    public class DidPlayerCancelChoosingSkill : Condition
    {
        private void OnValidate()
        {
            Description = "Is the player backing out of choosing a skill?";
        }

        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            return player.TurnManager.PressedCancelSkill;
        }
    }
}
