// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Change animation to a character's stance.
    /// </summary>
    [CreateAssetMenu(fileName = "Change Animation to Stance", menuName = "Behavior Tree Pattern/New Action/Common/Character/Animation/Change Animation to Stance")]
    public class ChangeAnimationToStance : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();
            character.ChangeAnimation("fightStance");
        }
    }
}
