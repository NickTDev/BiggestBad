// Nicholas Tvaroha

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the enemy can attack or move
    /// </summary>
    [CreateAssetMenu(fileName = "Cant Do Anything", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Cant Do Anything")]
    public class CantDoAnything : Condition
    {
        private void OnValidate()
        {
            Description = "Can the enemy move or attack?";
        }

        public override bool CheckCondition(StateManager state)
        {
            EnemyBattleController enemy = (EnemyBattleController)state;
            if (!enemy.IsAttacking && enemy.Stats.BaseSpeed <= 0) //Checks if the enemy can't attack and has no movement
                return true;

            return false;
        }
    }
}
