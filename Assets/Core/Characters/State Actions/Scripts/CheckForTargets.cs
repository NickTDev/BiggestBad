// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks for targets.
    /// </summary>
    [CreateAssetMenu(fileName = "Check for Targets", menuName = "Behavior Tree Pattern/New Action/Common/Character/Check for Targets")]
    public class CheckForTargets : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.CheckForTargets();
        }
    }
}
