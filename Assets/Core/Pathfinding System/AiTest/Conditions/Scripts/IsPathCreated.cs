//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if the pathfinding worked
    /// </summary>
    [CreateAssetMenu(fileName = "Path Created", menuName = "Behavior Tree Pattern/New Condition/Common/Enemy/Path Created")]
    public class IsPathCreated : Condition
    {
        private void OnValidate()
        {
            Description = "Is there a valid path?";
        }

        public override bool CheckCondition(StateManager state)
        {
            if (GameObject.Find("BattlefieldManager").GetComponent<WorldGrid>().FinalPath != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
