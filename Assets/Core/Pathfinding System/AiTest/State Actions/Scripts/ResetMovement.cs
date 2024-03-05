// Nicholas Tvaroha

using System.Collections.Generic;
using UnityEngine;
using T02.PathSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Resets the current movement back to the character's max
    /// </summary>
    [CreateAssetMenu(fileName = "Reset Movement", menuName = "Behavior Tree Pattern/New Action/Common/Character/Reset Movement", order = 0)]
    public class ResetMovement : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.Stats.ResetMovement();
        }
    }
}
