//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Move the character one step along the calculated path
    /// </summary>
    [CreateAssetMenu(fileName = "Enemy Move", menuName = "Behavior Tree Pattern/New Action/Common/Enemy/Enemy Move")]
    public class EnemyMove : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnemyBattleController enemy = (EnemyBattleController)states;

            //Movement Code
            List<Node> path = enemy.TurnManager.BattleGrid.FinalPath;
            var step = enemy.GridMoveSpeed * Time.deltaTime;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, new Vector3(path[0].position.x, enemy.transform.position.y, path[0].position.z), step);
        }
    }
}
