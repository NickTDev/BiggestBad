// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Resets the countered bool.
    /// </summary>
    [CreateAssetMenu(fileName = "Reset Countered", menuName = "Behavior Tree Pattern/New Action/Common/Enemy/Reset Countered")]
    public class ResetCountered : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnemyBattleController character = (EnemyBattleController)states;
            character.HasBeenCountered = false;
        }
    }
}