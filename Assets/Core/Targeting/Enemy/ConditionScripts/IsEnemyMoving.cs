// Nicholas Tvaroha

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the enemy is moving.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Enemy Moving", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Is Enemy Moving")]
    public class IsEnemyMoving : Condition
    {
        private void OnValidate()
        {
            Description = "Is the enemy moving?";
        }

        public override bool CheckCondition(StateManager state)
        {
            EnemyBattleController enemy = (EnemyBattleController)state;
            return enemy.IsMoving;
        }
    }
}
