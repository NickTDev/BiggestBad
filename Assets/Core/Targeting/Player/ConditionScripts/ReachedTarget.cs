//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if the character has finished moving
    /// </summary>
    [CreateAssetMenu(fileName = "Reached Target", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Reaced Target")]
    public class ReachedTarget : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            if (Mathf.Abs(player.transform.position.x - player.TileSelector.transform.position.x) <= 
                0.1f && Mathf.Abs(player.transform.position.z - player.TileSelector.transform.position.z) <= 0.1f)
                return true;
            else
                return false;
        }
    }
}
