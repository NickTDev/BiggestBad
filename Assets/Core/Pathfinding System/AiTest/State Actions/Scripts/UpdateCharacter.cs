// Nicholas Tvaroha

using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Moves the character towards their target.
    /// </summary>
    [CreateAssetMenu(fileName = "Update Character", menuName = "Behavior Tree Pattern/New Action/Common/Character/Update Character", order = 0)]
    public class UpdateCharacter : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnemyBattleController enemy = (EnemyBattleController)states;
            List<Node> path = enemy.TurnManager.BattleGrid.FinalPath;
            if (path[0].condition.conditionType == ConditionType.SANDY)
                enemy.Stats.SpendMovement(2);
            else
                enemy.Stats.SpendMovement(1);
            enemy.TurnManager.BattleGrid.FinalPath.Remove(path[0]);
        }
    }
}
