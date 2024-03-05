// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the character has died.
    /// </summary>
    [CreateAssetMenu(fileName = "Has Died", menuName = "Behavior Tree Pattern/New Condition/Common/Character/Has Died")]
    public class HasDied : Condition
    {
        private void OnValidate()
        {
            Description = "Is the character's HP 0?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CharacterBattleController character = (CharacterBattleController)state;

            return character.HasDied;
        }
    }
}
