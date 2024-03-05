// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player is attacking.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Player Attacking", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Is Player Attacking")]
    public class IsPlayerAttacking : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            return player.TurnManager.PressedSkillOptionInMenu;
        }
    }
}
