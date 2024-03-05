// Nicholas Tvaroha

using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Calculates the path from the player to the player target
    /// </summary>
    [CreateAssetMenu(fileName = "Player Calculate Path", menuName = "Behavior Tree Pattern/New Action/Placeholder/Player Calculate Path", order = 0)]
    public class PlayerCalculatePath : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController player = (PlayerBattleController)states;

            //GameObject.Find("BattlefieldManager").GetComponent<WorldGrid>().ClearGrid();
            Vector3 newTarget = new Vector3(player.TileSelector.transform.position.x, 0, player.TileSelector.transform.position.z);
            player.TurnManager.BattlePathfinding.FindPath(player.transform.position, newTarget);
        }
    }
}
