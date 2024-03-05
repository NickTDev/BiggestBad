// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Fires needle barrage at a targeted opponent.
    /// </summary>
    [CreateAssetMenu(fileName = "Fire Needles", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Fire Needles")]
    public class FireNeedles : StateActions
    {
        public override void Execute(StateManager states)
        {
            Needles attack = (Needles)states;
            EnemyBattleController enemy = attack.GetCharacter() as EnemyBattleController;

            enemy.ChangeAnimation("needlesThrow");
            attack.SpawnNeedles();
            enemy.SoundManager.PlayOneShot(attack._needleSound);
        }
    }
}