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
    [CreateAssetMenu(fileName = "Reached Path", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Reached Path")]
    public class ReachedPath : Condition
    {
        private void OnValidate()
        {
            Description = "Did the player reach the next tile in the path?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CharacterBattleController character = (CharacterBattleController)state;
            if (character.transform.position.x == GameObject.Find("BattlefieldManager").GetComponent<WorldGrid>().FinalPath[0].position.x && 
                character.transform.position.z == GameObject.Find("BattlefieldManager").GetComponent<WorldGrid>().FinalPath[0].position.z)
                return true;
            else
                return false;
        }
    }
}
