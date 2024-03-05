// Merle Roji

using UnityEngine;
using UnityEngine.TextCore.Text;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player pressed confirm.
    /// </summary>
    [CreateAssetMenu(fileName = "Target Confirm Pressed", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Target Confirm Pressed")]
    public class TargetConfirmPressed : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;
            RaycastHit hit;

            // check if in range of the skill
            if (Vector3.Distance(player.transform.position, player.TileSelector.transform.position) <=
                    player.SkillList[player.TurnManager.SkillMenuSelection].Range)
            {
                player.ValidTileColoring(true);

                //Debug.Log("Confirm Pressed");
                if (player.TargetConfirmPressed)
                {
                    // if tile is under an enemy
                    Physics.Raycast(player.TileSelector.transform.position + (Vector3.up * 2.0f), Vector3.down, out hit, 5.0f);
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.TryGetComponent<CharacterBattleController>(out CharacterBattleController potentialTarget);
                        player.PotentialTargets.Add(potentialTarget);
                        return true;
                    }
                }
            }
            else
            {
                player.ValidTileColoring(false);
            }

            return false;
        }
    }
}
