// Merle Roji

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Logic for explosion attacks.
    /// </summary>
    public abstract class ExplosionAttackBase : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField, Min(0f)] protected float _destroyTimer = 1f;
        [SerializeField, Min(0f)] protected float _height = 1f;
        [SerializeField] protected GameObject[] _boxes;
        protected MinigameStateMachine _bomb;
        protected int _damage;
        protected int _attackStat;
        protected int _amountOfTurns;
        protected bool _canDealDamage = false;
        protected bool _dealtDamage = false;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _canDealDamage = false;
            _dealtDamage = false;
            Destroy(gameObject, _destroyTimer);
        }

        private void OnValidate()
        {
            foreach (GameObject box in _boxes)
            {
                box.transform.localScale = new Vector3(box.transform.localScale.x, _height, box.transform.localScale.z);
            }
        }

        private void OnTriggerStay(Collider col)
        {
            if (_canDealDamage)
            {
                if (!_dealtDamage)
                {
                    ExplosionCollision(col);
                }
            }
        }

        #endregion

        #region EXPLOSION BASE METHODS

        public virtual void InjectDamage(MinigameStateMachine bomb, int damage, int attackStat, int amountOfTurns = 0)
        {
            _bomb = bomb;
            _damage = damage;
            _attackStat = attackStat;
            _amountOfTurns = amountOfTurns;
            _canDealDamage = true;
        }

        protected abstract void ExplosionCollision(Collider col);

        #endregion
    }
}