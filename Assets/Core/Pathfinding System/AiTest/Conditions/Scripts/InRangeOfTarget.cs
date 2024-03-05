//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if the enemy is within 2 diagonal spaces or 3 cardinal spaces of the player
    /// </summary>
    [CreateAssetMenu(fileName = "Entered Range", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Entered Range")]
    public class InRangeOfTarget : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            EnemyBattleController enemy = (EnemyBattleController)state;
            GameObject player = enemy.Target.gameObject;
            if (Vector3.Distance(enemy.transform.position, player.transform.position) < enemy.SkillList[enemy.ChosenAttackIndex].Range)
                return true;
            else
                return false;
        }
    }
}
