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
    [CreateAssetMenu(fileName = "No Reached Target", menuName = "Behavior Tree Pattern/New Condition/Common/Player/No Reached Target")]
    public class NoReachedTarget : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            if (player.transform.position.x == player.TileSelector.transform.position.x && 
                player.transform.position.z == player.TileSelector.transform.position.z)
                return false;
            else
                return true;
        }
    }
}
