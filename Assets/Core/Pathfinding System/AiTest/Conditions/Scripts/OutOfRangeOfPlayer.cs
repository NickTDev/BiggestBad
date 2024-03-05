//Nicholas Tvaroha

using System.Collections.Generic;
using T02.PathSystem;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if the enemy is within 2 diagonal spaces or 3 cardinal spaces of the player
    /// </summary>
    [CreateAssetMenu(fileName = "Out of Range", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Out of Range")]
    public class OutOfRangeOfPlayer : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            EnemyBattleController enemy = (EnemyBattleController)state;
            GameObject player = enemy.Target.gameObject;
            List<Node> path = enemy.TurnManager.BattleGrid.FinalPath;

            if (Vector3.Distance(enemy.transform.position, player.transform.position) < enemy.SkillList[enemy.ChosenAttackIndex].Range)
            {
                return false;
            }
            else if (enemy.Stats.CurrentMovement <= 0 || (path[0].condition.conditionType == ConditionType.SANDY && enemy.Stats.CurrentMovement <= 1))
            {
                return false;
            }
            else
                return true;
        }
    }
}
