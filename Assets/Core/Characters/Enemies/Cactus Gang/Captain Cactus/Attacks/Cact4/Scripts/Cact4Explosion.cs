// Merle Roji

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Logic for the Cact4 explosion.
    /// </summary>
    public class Cact4Explosion : ExplosionAttackBase
    {
        private Cact4Attack _cact4Bomb;

        public override void InjectDamage(MinigameStateMachine bomb, int damage, int attackStat, int amountOfTurns = 0)
        {
            base.InjectDamage(bomb, damage, attackStat, amountOfTurns);
            _cact4Bomb = _bomb as Cact4Attack;
        }

        protected override void ExplosionCollision(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                PlayerBattleController player = col.GetComponent<PlayerBattleController>();
                int damage = player.Stats.TakeDamage(_damage, _attackStat);
                _cact4Bomb.SpawnHitVFX(col.transform);
                _cact4Bomb.DisplayDamage(damage);
                Debug.Log(damage);

                _dealtDamage = true;
            }
        }
    }
}