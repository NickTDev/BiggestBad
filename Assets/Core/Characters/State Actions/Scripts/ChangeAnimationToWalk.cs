// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Change animation to a character's walk.
    /// </summary>
    [CreateAssetMenu(fileName = "Change Animation to Walk", menuName = "Behavior Tree Pattern/New Action/Common/Character/Animation/Change Animation to Walk")]
    public class ChangeAnimationToWalk : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.ChangeAnimation("walk");
        }
    }
}
