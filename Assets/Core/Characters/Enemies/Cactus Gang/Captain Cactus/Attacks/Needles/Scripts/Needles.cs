// Merle Roji

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Captain Cactus's basic attack.
    /// </summary>
    public class Needles : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Needles Parameters")]
        [SerializeField] private GameObject _needleBarragePrefab;
        private GameObject _needleBarrage = null;
        [SerializeField] private Color _fullChargeColor = Color.red;
        [SerializeField] public AudioClip _needleSound;

        #endregion

        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();

            _damage = _baseDamage;
        }

        #endregion

        #region HELPERS

        public Color FullChargeColor
        { get => _fullChargeColor; }

        public bool AreNeedlesFullyCharged
        { get => _character.CharacterSprite.color == _fullChargeColor; }

        public bool DidNeedlesReachTarget
        {
            get
            {
                if (_needleBarrage == null) return false; // return if no needles

                Vector3 needlesPos = _needleBarrage.transform.position; // store needles position
                float targetDiameter = _character.Target.GetComponent<CapsuleCollider>().radius * 2; // store target radius
                Vector3 targetPos = _character.Target.transform.position;
                float distance = Vector3.Distance(needlesPos, targetPos); // calc distance

                if (distance <= targetDiameter) return true;
                else return false;
            }
        }

        public bool AreNeedlesDestroyed
        { get => _needleBarrage == null; }

        /// <summary>
        /// Spawns the needle barrage object.
        /// </summary>
        public void SpawnNeedles()
        {
            if (_needleBarrage == null)
            {
                _character.AttackPivot.LookAt(_character.Target.transform);
                _needleBarrage = Instantiate(_needleBarragePrefab, _character.AttackPivot);
                Rigidbody needlesRb = _needleBarrage.GetComponent<Rigidbody>();
                needlesRb.velocity = _character.AttackPivot.forward * _moveSpeed;
            }
        }

        /// <summary>
        /// Destroys the needle barrage object.
        /// </summary>
        public void DestroyNeedles()
        {
            if (_needleBarrage != null)
            {
                Destroy(_needleBarrage.gameObject);
            }
        }

        public override void CheckForTargets(CharacterBattleController character)
        {
            /// COMPLETE PLACEHOLDER - REPLACE THIS WITH NEW TARGETING SYSTEM
            // get array of character colliders
            //Collider[] hitColliders = null;
            //hitColliders = Physics.OverlapSphere(character.transform.position, _range, _characterLayer, QueryTriggerInteraction.Collide);
            //Debug.Log("It's " + (hitColliders.Length > 0) + " that targets were found!!");
            //
            //// add the colliders into the potential target list
            //if (hitColliders != null)
            //{
            //    foreach (Collider col in hitColliders)
            //    {
            //        if (col.TryGetComponent<CharacterBattleController>(out CharacterBattleController potentialTarget) && potentialTarget != character)
            //        {
            //            Debug.Log(col.name + " is in range!");
            //            character.PotentialTargets.Add(potentialTarget);
            //        }
            //    }
            //
            //    // if the character is an enemy, remove other enemies from potential targets
            //    if (character is EnemyBattleController && character.PotentialTargets.Count > 0)
            //    {
            //        EnemyBattleController enemy = character as EnemyBattleController;
            //
            //        for (int i = 0; i < enemy.PotentialTargets.Count; i++)
            //        {
            //            if (enemy.PotentialTargets[i].tag != "Player")
            //                enemy.PotentialTargets.Remove(enemy.PotentialTargets[i]);
            //        }
            //
            //        enemy.SpawnSelector(enemy.PotentialTargets[0].transform.position);
            //    }
            //}
            ///
        }

        #endregion
    }
}