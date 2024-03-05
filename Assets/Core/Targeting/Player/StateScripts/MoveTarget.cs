// Nicholas Tvaroha

using Ink.Parsed;
using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Allows the player to move the target in the world
    /// </summary>
    [CreateAssetMenu(fileName = "Move Target", menuName = "Behavior Tree Pattern/New Action/Placeholder/Move Target")]
    public class MoveTarget : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController player = (PlayerBattleController)states;
            Vector2 dpadMove = player.DPadMovement.normalized;

            if (player.PressedMove)
            {
                if (dpadMove.x == 0f &&
                    dpadMove.y == 0f)
                    player.PressedMove = false;
            }

            if (!player.PressedMove)
            {
                if (dpadMove.y >= 1) // up
                {
                    RaycastHit aHit;
                    if (Physics.Raycast(player.TileSelector.transform.position, Vector3.forward, out aHit, 1.0f))
                    {
                        if (aHit.transform.tag != "Obstacle")
                        {
                            player.TileSelector.transform.position += Vector3.forward;
                            player.PressedMove = true;
                        }
                    }
                    else
                    {
                        player.TileSelector.transform.position += Vector3.forward;
                        player.PressedMove = true;
                    }
                }
                if (dpadMove.x <= -1) // left
                {
                    RaycastHit aHit;
                    if (Physics.Raycast(player.TileSelector.transform.position, Vector3.left, out aHit, 1.0f))
                    {
                        if (aHit.transform.tag != "Obstacle")
                        {
                            player.TileSelector.transform.position += Vector3.left;
                            player.PressedMove = true;
                        }
                    }
                    else
                    {
                        player.TileSelector.transform.position += Vector3.left;
                        player.PressedMove = true;
                    }
                }
                if (dpadMove.y <= -1) // down
                {
                    RaycastHit aHit;
                    if (Physics.Raycast(player.TileSelector.transform.position, Vector3.back, out aHit, 1.0f))
                    {
                        if (aHit.transform.tag != "Obstacle")
                        {
                            player.TileSelector.transform.position += Vector3.back;
                            player.PressedMove = true;
                        }
                    }
                    else
                    {
                        player.TileSelector.transform.position += Vector3.back;
                        player.PressedMove = true;
                    }
                }
                if (dpadMove.x >= 1) // right
                {
                    RaycastHit aHit;
                    if (Physics.Raycast(player.TileSelector.transform.position, Vector3.right, out aHit, 1.0f))
                    {
                        if (aHit.transform.tag != "Obstacle")
                        {
                            player.TileSelector.transform.position += Vector3.right;
                            player.PressedMove = true;
                        }
                    }
                    else
                    {
                        player.TileSelector.transform.position += Vector3.right;
                        player.PressedMove = true;
                    }
                }
            }

            RaycastHit hit;
            if (Physics.Raycast(player.TileSelector.transform.position, Vector3.up, out hit, 5.0f) || 
                Physics.Raycast(player.TileSelector.transform.position, Vector3.down, out hit, 5.0f))
            {
                if (hit.transform.tag != "Player" && hit.transform.tag != "Enemy")
                {
                    Vector3 newPos = NewHitPos(hit);
                    player.TileSelector.transform.position = newPos + (Vector3.up * ((hit.transform.GetComponent<BoxCollider>().size.y / 2) + 0.05f));
                }
            }
        }

        private Vector3 NewHitPos(RaycastHit hit)
        {
            Vector3 newPos = new Vector3(
                hit.transform.position.x,
                hit.transform.position.y +
                hit.transform.GetComponent<BoxCollider>().center.y,
                hit.transform.position.z
                );


            return newPos;
        }
    }
}
