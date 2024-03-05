// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Disables the player controls.
    /// </summary>
    [CreateAssetMenu(fileName = "Disable Battle Controls", menuName = "Behavior Tree Pattern/New Action/Common/Player/Disable Battle Controls")]
    public class DisablePlayerBattleControls : StateActions
    {
        public override void Execute(StateManager states)
        {
            if (states is PlayerBattleController)
            {
                PlayerBattleController player = (PlayerBattleController)states;
                player.DisableBattleControls();
            }

            if (states is EnemyBattleController)
            {
                EnemyBattleController enemy = (EnemyBattleController)states;
                PlayerBattleController player = enemy.Target as PlayerBattleController;
                player.DisableBattleControls();
            }
        }
    }
}
