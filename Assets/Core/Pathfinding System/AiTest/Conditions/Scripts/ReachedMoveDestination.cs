//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if the character has finished moving to the appropriate grid square
    /// </summary>
    [CreateAssetMenu(fileName = "Reached Move Dest", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Reached Move Dest")]
    public class ReachedMoveDestination : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            EnemyBattleController enemy = (EnemyBattleController)state;
            if (enemy.transform.position.x == enemy.TurnManager.BattleGrid.FinalPath[0].position.x && 
                enemy.transform.position.z == enemy.TurnManager.BattleGrid.FinalPath[0].position.z)
                return true;
            else
                return false;
        }
    }
}
