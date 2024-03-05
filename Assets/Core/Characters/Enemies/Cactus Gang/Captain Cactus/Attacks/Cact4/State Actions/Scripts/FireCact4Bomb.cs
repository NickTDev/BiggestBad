// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Fires Cact4 bomb at a targeted opponent.
    /// </summary>
    [CreateAssetMenu(fileName = "Fire Cact4 Bomb", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Fire Cact4 Bomb")]
    public class FireCact4Bomb : StateActions
    {
        public override void Execute(StateManager states)
        {
            Cact4Attack attack = (Cact4Attack)states;
            EnemyBattleController enemy = attack.GetCharacter() as EnemyBattleController;

            enemy.ChangeAnimation("cact4Throw");
            attack.SpawnBomb();
        }
    }
}