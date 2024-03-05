// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the player is backing out of choosing a movement.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Player Cancel Choosing Move", menuName = "Behavior Tree Pattern/New Condition/Common/Menu/Did Player Cancel Choosing Move")]
    public class DidPlayerCancelChoosingMove : Condition
    {
        private void OnValidate()
        {
            Description = "Is the player backing out of moving?";
        }

        public override bool CheckCondition(StateManager state)
        {
            PlayerBattleController player = (PlayerBattleController)state;

            return player.TargetCancelPressed;
        }
    }
}
