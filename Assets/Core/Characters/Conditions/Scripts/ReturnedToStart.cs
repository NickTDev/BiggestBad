// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the character has returned to their starting position.
    /// </summary>
    [CreateAssetMenu(fileName = "Returned to Start", menuName = "Behavior Tree Pattern/New Condition/Common/Character/Returned to Start")]
    public class ReturnedToStart : Condition
    {
        private void OnValidate()
        {
            Description = "Returned to starting position?";
        }

        public override bool CheckCondition(StateManager state)
        {
            MinigameStateMachine attack = (MinigameStateMachine)state;
            CharacterBattleController character = attack.GetCharacter();

            return character.transform.position == attack.InitialPosition;
        }
    }
}