// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player pressed Player 1 Action.
    /// </summary>
    [CreateAssetMenu(fileName = "Was P1 Action Pressed", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Was P1 Action Pressed")]
    public class WasPressedPlayer1Action : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            MinigameStateMachine attack = null;
            PlayerBattleController player = null;

            if (state is MinigameStateMachine)
            {
                attack = (MinigameStateMachine)state;
                player = attack.GetCharacter() as PlayerBattleController;
            }
            else if (state is PlayerBattleController)
            {
                player = (PlayerBattleController)state;
            }

            return player.Player1ActionPressed;
        }
    }
}
