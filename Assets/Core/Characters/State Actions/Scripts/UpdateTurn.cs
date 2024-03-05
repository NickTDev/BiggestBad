// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Updates the turn the character is on.
    /// </summary>
    [CreateAssetMenu(fileName = "Update Turn", menuName = "Behavior Tree Pattern/New Action/Common/Character/Update Turn", order = 0)]
    public class UpdateTurn : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.IsTurn = character.TurnInfo.IsTurn;
        }
    }
}