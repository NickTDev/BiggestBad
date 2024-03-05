// Nicholas Tvaroha

using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Updates the path and character movement
    /// </summary>
    [CreateAssetMenu(fileName = "Update Path", menuName = "Behavior Tree Pattern/New Action/Placeholder/Update Path", order = 0)]
    public class UpdatePath : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController player = (PlayerBattleController)states;
            List<Node> path = player.TurnManager.BattleGrid.FinalPath;

            if (path[0] != null)
            {
                //if (path[0].condition.conditionType == ConditionType.SANDY)
                //    player.Stats.SpendMovement(2);
                //else
                //    player.Stats.SpendMovement(1);
                player.TurnManager.BattleGrid.FinalPath.Remove(path[0]);
                Debug.Log(player.Stats.CurrentMovement);
            }
        }
    }
}
