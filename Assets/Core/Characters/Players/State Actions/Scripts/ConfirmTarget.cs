// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Confirms the selected target.
    /// </summary>
    [CreateAssetMenu(fileName = "Confirm Target", menuName = "Behavior Tree Pattern/New Action/Common/Player/Confirm Target")]
    public class ConfirmTarget : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;

            character.ConfirmTarget();
        }
    }
}