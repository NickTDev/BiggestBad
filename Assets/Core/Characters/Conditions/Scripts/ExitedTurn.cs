// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player has exited their turn.
    /// </summary>
    [CreateAssetMenu(fileName = "Exited Turn", menuName = "Behavior Tree Pattern/New Condition/Common/Character/Exited Turn")]
    public class ExitedTurn : Condition
    {
        private void OnValidate()
        {
            Description = "Is it not this character's turn?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CharacterBattleController character = (CharacterBattleController)state;

            return !character.TurnInfo.IsTurn;
        }
    }
}
