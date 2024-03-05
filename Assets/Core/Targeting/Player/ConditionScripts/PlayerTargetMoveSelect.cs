//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using T02.PathSystem;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if the player clicks to select where they will move
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerTargetMoveSelect", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Player Target Move Select")]
    public class PlayerTargetMoveSelect : Condition
    {
        private void OnValidate()
        {
            Description = "Is the selected tile valid?";
        }

        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;
            //GameObject target = GameObject.Find("PlayerTarget(Clone)");
            RaycastHit hit;

            if (player.Stats.CurrentMovement <= 0)
            {
                player.ValidTileColoring(false);
            }

            if (player.PressedMove)
            {
                Physics.Raycast(player.TileSelector.transform.position, Vector3.down, out hit, 5.0f);

                if (player.TurnManager.BattleGrid.NodeFromWorldPosition(hit.transform.position).walkable)
                {
                    Physics.Raycast(player.TileSelector.transform.position + (Vector3.up * 2.0f), Vector3.down, out hit, 5.0f);
                    if (hit.transform.tag != "Player" && hit.transform.tag != "Enemy")
                    {
                        Vector3 newTarget = new Vector3(player.TileSelector.transform.position.x, 0, player.TileSelector.transform.position.z);
                        player.TurnManager.BattlePathfinding.FindPath(player.transform.position, newTarget);
                        List<Node> path = player.TurnManager.BattleGrid.FinalPath;

                        // check if the player has enough movement to get there.
                        int cost = path[path.Count - 1].gCost;
                        if (cost <= player.Stats.CurrentMovement)
                        {
                            player.ValidTileColoring(true);
                        }
                        else
                        {
                            player.ValidTileColoring(false);
                        }
                    }
                }
            }

            if (player.TargetConfirmPressed)
            {
                Physics.Raycast(player.TileSelector.transform.position, Vector3.down, out hit, 5.0f);
            
                if (player.TurnManager.BattleGrid.NodeFromWorldPosition(hit.transform.position).walkable)
                {
                    Physics.Raycast(player.TileSelector.transform.position + (Vector3.up * 2.0f), Vector3.down, out hit, 5.0f);
                    if (hit.transform.tag != "Player" && hit.transform.tag != "Enemy")
                    {
                        Vector3 newTarget = new Vector3(player.TileSelector.transform.position.x, 0, player.TileSelector.transform.position.z);
                        player.TurnManager.BattlePathfinding.FindPath(player.transform.position, newTarget);
                        List<Node> path = player.TurnManager.BattleGrid.FinalPath;
            
                        // check if the player has enough movement to get there.
                        int cost = path[path.Count - 1].gCost;
                        if (cost <= player.Stats.CurrentMovement)
                        {
                            player.Stats.SpendMovement(cost);
                            Debug.Log(cost);

                            if (path[path.Count - 1].position.x > player.transform.position.x)
                            {
                                player.CharacterSprite.flipX = false;
                            }
                            else if (path[path.Count - 1].position.x < player.transform.position.x)
                            {
                                player.CharacterSprite.flipX = true;
                            }

                            if (player.TurnManager.TutorialIndex <= 0)
                                player.TurnManager.TutorialIndex++;

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
