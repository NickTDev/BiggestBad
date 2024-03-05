// Nicholas Tvaroha

using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Exits the character's turn
    /// </summary>
    [CreateAssetMenu(fileName = "Exit Turn", menuName = "Behavior Tree Pattern/New Action/Common/Enemy/Exit Turn", order = 0)]
    public class ExitTurn : StateActions
    {
        public override void Execute(StateManager states)
        {
            Debug.Log("Exit Turn Called");
            EnemyBattleController enemy = (EnemyBattleController)states;
            enemy.FinishTurn();
        }
    }
}
