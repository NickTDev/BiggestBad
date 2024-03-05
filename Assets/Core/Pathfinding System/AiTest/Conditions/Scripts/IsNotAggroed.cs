//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if the enemy is not aggroed
    /// </summary>
    [CreateAssetMenu(fileName = "Is Not Aggroed", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Is Not Aggroed")]
    public class IsNotAggroed : Condition
    {
        private void OnValidate()
        {
            Description = "Is the enemy not aggroed?";
        }

        public override bool CheckCondition(StateManager state)
        {
            EnemyBattleController enemy = (EnemyBattleController)state;
            return !enemy.IsAggroed;
        }
    }
}
