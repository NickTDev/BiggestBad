// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the enemy got countered.
    /// </summary>
    [CreateAssetMenu(fileName = "Got Countered", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Got Countered")]
    public class GotCountered : Condition
    {
        private void OnValidate()
        {
            Description = "Countered by Player?";
        }

        public override bool CheckCondition(StateManager state)
        {
            MinigameStateMachine attack = (MinigameStateMachine)state;
            EnemyBattleController character = attack.GetCharacter() as EnemyBattleController;

            return character.HasBeenCountered;
        }
    }
}
