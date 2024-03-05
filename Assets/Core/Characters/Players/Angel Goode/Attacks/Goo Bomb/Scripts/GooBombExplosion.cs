// Merle Roji

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Logic for the Goo Bomb's explosion.
    /// </summary>
    public class GooBombExplosion : ExplosionAttackBase
    {
        private GooBomb _gooBombAttack;

        public override void InjectDamage(MinigameStateMachine bomb, int damage, int attackStat, int amountOfTurns = 0)
        {
            base.InjectDamage(bomb, damage, attackStat, amountOfTurns);
            _gooBombAttack = _bomb as GooBomb;
        }

        protected override void ExplosionCollision(Collider col)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyBattleController enemy = col.GetComponent<EnemyBattleController>();
                int damage = enemy.Stats.TakeDamage(_damage, _attackStat);
                _gooBombAttack.SpawnHitVFX(col.transform);
                _gooBombAttack.DisplayDamage(damage);
                Debug.Log(damage);

                if (_amountOfTurns > 0)
                    enemy.AddNewEffect(new StuckEffect(enemy, _amountOfTurns));

                _dealtDamage = true;
            }
        }
    }
}