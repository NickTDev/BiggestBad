// Nicholas Tvaroha

using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Calculates the path from the enemy to the character
    /// </summary>
    [CreateAssetMenu(fileName = "Calculate Path", menuName = "Behavior Tree Pattern/New Action/Common/Enemy/Calculate Path", order = 0)]
    public class CalculatingPath : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnemyBattleController enemy = (EnemyBattleController)states;
            GameObject player = enemy.Target.gameObject;

            enemy.TurnManager.BattlePathfinding.FindPath(enemy.transform.position, player.transform.position);
        }
    }
}
