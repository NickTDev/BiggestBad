// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Change animation to a character's idle.
    /// </summary>
    [CreateAssetMenu(fileName = "Change Animation to Idle", menuName = "Behavior Tree Pattern/New Action/Common/Character/Animation/Change Animation to Idle")]
    public class ChangeAnimationToIdle : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.ChangeAnimation("idle");
        }
    }
}
