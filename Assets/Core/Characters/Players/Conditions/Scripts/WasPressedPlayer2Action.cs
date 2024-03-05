// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player pressed Player 2 Action.
    /// </summary>
    [CreateAssetMenu(fileName = "Was P2 Action Pressed", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Was P2 Action Pressed")]
    public class WasPressedPlayer2Action : Condition
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

            return player.Player2ActionPressed;
        }
    }
}
