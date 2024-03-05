// Merle Roji

using UnityEngine;
using TMPro;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Detonates the Cact4 Bomb.
    /// </summary>
    [CreateAssetMenu(fileName = "Detonate Cact4", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Detonate Cact4")]
    public class DetonateCact4 : StateActions
    {
        public override void Execute(StateManager states)
        {
            Cact4Attack attack = (Cact4Attack)states;
            EnemyBattleController enemy = attack.GetCharacter() as EnemyBattleController;
            attack.SpawnExplosion();
            enemy.SoundManager.PlayOneShot(attack._explosionSound);
        }
    }
}