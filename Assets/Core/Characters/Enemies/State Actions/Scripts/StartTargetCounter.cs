// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Starts the players' counter minigame.
    /// </summary>
    [CreateAssetMenu(fileName = "Start Target Counter", menuName = "Behavior Tree Pattern/New Action/Common/Enemy/Start Target Counter")]
    public class StartTargetCounter : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            EnemyBattleController enemy = attack.GetCharacter() as EnemyBattleController;
            
            // start counter for all characters
            foreach(PlayerBattleController player in enemy.TurnManager.PlayerParty)
            {
                if (player.Stats.CurrentHP > 0)
                    player.StartCounter(enemy);
            }
        }
    }
}