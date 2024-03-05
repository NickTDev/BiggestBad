// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the enemy is attacking.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Enemy Attacking", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Is Enemy Attacking")]
    public class IsEnemyAttacking : Condition
    {
        private void OnValidate()
        {
            Description = "Is the enemy attacking?";
        }

        public override bool CheckCondition(StateManager state)
        {
            EnemyBattleController enemy = (EnemyBattleController)state;
            return enemy.IsAttacking;
        }
    }
}
