// Merle Roji

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Captain Cactus's Cact4 attack.
    /// </summary>
    public class Cact4Attack : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Cact-4 Parameters")]
        [SerializeField] private GameObject _bombPrefab;
        private GameObject _bomb = null;
        [SerializeField] private GameObject _explosionPrefab;
        private GameObject _explosion = null;
        [SerializeField, Min(0f)] private float _dangerDistance = 1f;
        [SerializeField] public AudioClip _explosionSound;

        #endregion

        #region HELPERS

        public GameObject Bomb
        { get => _bomb; }

        public bool DidBombReachTarget
        {
            get
            {
                if (_bomb == null) return false; // return if no bomb

                Vector3 bombPos = _bomb.transform.position; // store bomb position
                float targetDiameter = _character.Target.GetComponent<CapsuleCollider>().radius * 2; // store target radius
                Vector3 targetPos = _character.Target.transform.position;
                float distance = Vector3.Distance(bombPos, targetPos); // calc distance

                if (distance <= targetDiameter) return true;
                else return false;
            }
        }

        public bool DidBombReachDangerZone
        {
            get
            {
                if (_bomb == null) return false; // return if no bomb

                Vector3 bombPos = _bomb.transform.position; // store bomb position
                float targetDiameter = _character.Target.GetComponent<CapsuleCollider>().radius * 2; // store target radius
                Vector3 targetPos = _character.Target.transform.position;
                float distance = Vector3.Distance(bombPos, targetPos); // calc distance

                if (distance <= targetDiameter + _dangerDistance) return true;
                else return false;
            }
        }

        /// <summary>
        /// Spawns the bomb object.
        /// </summary>
        public void SpawnBomb()
        {
            if (_bomb == null)
            {
                _character.AttackPivot.LookAt(_character.Target.transform);
                _bomb = Instantiate(_bombPrefab, _character.AttackPivot);
                Rigidbody grenadeRb = _bomb.GetComponent<Rigidbody>();
                grenadeRb.velocity = _character.AttackPivot.forward * _moveSpeed;
            }
        }

        /// <summary>
        /// Destroys the grenade object.
        /// </summary>
        public void DestroyBomb()
        {
            if (_bomb != null)
            {
                Destroy(_bomb.gameObject);
            }
        }

        /// <summary>
        /// Deletes the bomb and spawns the explosion.
        /// </summary>
        public void SpawnExplosion()
        {
            if (_explosion == null && _bomb != null)
            {
                _explosion = Instantiate(_explosionPrefab, _initialTargetPosition, Quaternion.identity);
                _explosion.GetComponent<Cact4Explosion>().InjectDamage(this, BaseDamage, _character.Stats.CurrentAttack);
                DestroyBomb();
            }
        }

        public void DisplayDamage(int damage)
        {
            // show damage panel
            if (!DamagePanel.activeInHierarchy)
                DamagePanel.SetActive(true);
            TextMeshProUGUI damageText = DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

            // display damage
            damageText.text = "Damage: " + damage;
        }

        #endregion
    }
}