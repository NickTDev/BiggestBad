// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Finishes the turn.
    /// </summary>
    [CreateAssetMenu(fileName = "Finish Turn", menuName = "Behavior Tree Pattern/New Action/Common/Character/Finish Turn")]
    public class FinishTurn : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.FinishTurn();
        }
    }
}
