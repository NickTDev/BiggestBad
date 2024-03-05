// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Enables the player battle controls.
    /// </summary>
    [CreateAssetMenu(fileName = "Enable Battle Controls", menuName = "Behavior Tree Pattern/New Action/Common/Player/Enable Battle Controls")]
    public class EnablePlayerBattleControls : StateActions
    {
        public override void Execute(StateManager states)
        {
            if (states is PlayerBattleController)
            {
                PlayerBattleController player = (PlayerBattleController)states;
                player.EnableBattleControls();
            }

            if (states is EnemyBattleController)
            {
                EnemyBattleController enemy = (EnemyBattleController)states;
                PlayerBattleController player = enemy.Target as PlayerBattleController;
                player.EnableBattleControls();
            }
        }
    }
}
