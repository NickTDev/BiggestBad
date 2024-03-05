// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player pressed cancel during targeting.
    /// </summary>
    [CreateAssetMenu(fileName = "Target Cancel Pressed", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Target Cancel Pressed")]
    public class TargetCancelPressed : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;
            return player.TargetCancelPressed;
        }
    }
}
