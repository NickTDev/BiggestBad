// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Moves the character towards their target.
    /// </summary>
    [CreateAssetMenu(fileName = "Move Towards Target", menuName = "Behavior Tree Pattern/New Action/Common/Character/Move Towards Target", order = 0)]
    public class MoveTowardsTarget : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            var step = attack.MoveSpeed * Time.fixedDeltaTime;
            character.transform.position = Vector3.MoveTowards(character.transform.position, attack.InitialTargetPosition, step);
        }
    }
}