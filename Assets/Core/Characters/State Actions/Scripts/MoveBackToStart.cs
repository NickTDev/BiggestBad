// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Moves the character towards their starting position.
    /// </summary>
    [CreateAssetMenu(fileName = "Move Back to Start", menuName = "Behavior Tree Pattern/New Action/Common/Character/Move Back to Start", order = 0)]
    public class MoveBackToStart : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            var step = attack.MoveSpeed * Time.fixedDeltaTime;
            character.transform.position = Vector3.MoveTowards(character.transform.position, attack.InitialPosition, step);
        }
    }
}