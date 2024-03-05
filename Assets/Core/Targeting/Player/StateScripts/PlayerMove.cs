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
    [CreateAssetMenu(fileName = "Player Move", menuName = "Behavior Tree Pattern/New Action/Placeholder/Player Move")]
    public class PlayerMove : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController player = (PlayerBattleController)states;

            //Movement Code
            List<Node> path = player.TurnManager.BattleGrid.FinalPath;
            var step = player.GridMoveSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(path[0].position.x, 1, path[0].position.z), step);
        }
    }
}
