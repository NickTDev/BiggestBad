// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the character has collided with their target.
    /// </summary>
    [CreateAssetMenu(fileName = "Collided with Target", menuName = "Behavior Tree Pattern/New Condition/Common/Character/Collided with Target")]
    public class CollidedWithTarget : Condition
    {
        private void OnValidate()
        {
            Description = "Collided with target character?";
        }

        public override bool CheckCondition(StateManager state)
        {
            MinigameStateMachine attack = (MinigameStateMachine)state;
            CharacterBattleController character = attack.GetCharacter();

            return character.HasCollidedWithTarget;
        }
    }
}
