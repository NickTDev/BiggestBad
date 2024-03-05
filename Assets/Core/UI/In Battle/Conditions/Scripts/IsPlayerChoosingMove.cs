// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player is choosing to move.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Player Choosing Move", menuName = "Behavior Tree Pattern/New Condition/Common/Menu/Is Player Choosing Move")]
    public class IsPlayerChoosingMove : Condition
    {
        private void OnValidate()
        {
            Description = "Is the player choosing to move?";
        }

        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            return player.TurnManager.PressedMoveOptionInMenu;
        }
    }
}
