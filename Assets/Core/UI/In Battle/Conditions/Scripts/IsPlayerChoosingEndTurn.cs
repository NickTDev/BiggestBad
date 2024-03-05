// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player is choosing to end their turn.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Player Choosing End Turn", menuName = "Behavior Tree Pattern/New Condition/Common/Menu/Is Player Choosing End Turn")]
    public class IsPlayerChoosingEndTurn : Condition
    {
        private void OnValidate()
        {
            Description = "Is the player choosing to end turn?";
        }

        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            if (player.TurnManager.PressedEndTurnOptionInMenu)
            {
                player.FinishTurn();
                return true;
            }

            return false;
        }
    }
}
