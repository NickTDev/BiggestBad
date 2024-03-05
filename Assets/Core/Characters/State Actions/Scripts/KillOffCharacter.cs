// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Kills the character.
    /// </summary>
    [CreateAssetMenu(fileName = "Kill Off", menuName = "Behavior Tree Pattern/New Action/Common/Character/Kill Off")]
    public class KillOffCharacter : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.KillCharacter();
        }
    }
}
