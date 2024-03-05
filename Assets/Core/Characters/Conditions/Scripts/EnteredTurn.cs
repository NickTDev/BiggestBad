// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player has entered their turn.
    /// </summary>
    [CreateAssetMenu(fileName = "Entered Turn", menuName = "Behavior Tree Pattern/New Condition/Common/Character/Entered Turn")]
    public class EnteredTurn : Condition
    {
        private void OnValidate()
        {
            Description = "Is it this character's turn?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CharacterBattleController character = (CharacterBattleController)state;

            return character.TurnInfo.IsTurn;
        }
    }
}
