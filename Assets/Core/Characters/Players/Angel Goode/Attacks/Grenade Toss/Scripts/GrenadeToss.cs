// Merle Roji

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Angel Goode's basic attack.
    /// </summary>
    public class GrenadeToss : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Grenade Toss Parameters")]
        [SerializeField] private GameObject _grenadePrefab;
        private GameObject _grenade = null;
        [SerializeField] private Slider _attackSlider;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] public AudioClip _throwSound;
        [SerializeField] public AudioClip _explodeSound;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            CurrentState.OnEnterExecute(this);
        }

        private void OnValidate()
        {
            _attackSlider.maxValue = _maxTime;
        }

        #endregion

        #region HELPERS

        public Slider AttackSlider
        { get => _attackSlider; }

        public bool DidGrenadeReachTarget
        {
            get
            {
                if (_grenade == null) return false; // return if no grenade

                Vector3 grenadePos = _grenade.transform.position; // store grenade position
                float targetDiameter = _character.Target.GetComponent<CapsuleCollider>().radius * 2; // store target radius
                Vector3 targetPos = _character.Target.transform.position;
                float distance = Vector3.Distance(grenadePos, targetPos); // calc distance

                if (distance <= targetDiameter) return true;
                else return false;
            }
        }

        /// <summary>
        /// Spawns the grenade object.
        /// </summary>
        public void SpawnGrenade()
        {
            if (_grenade == null)
            {
                _character.AttackPivot.LookAt(_character.Target.transform);
                _grenade = Instantiate(_grenadePrefab, _character.AttackPivot);
                Rigidbody grenadeRb = _grenade.GetComponent<Rigidbody>();
                grenadeRb.velocity = _character.AttackPivot.forward * _moveSpeed;
            }
        }

        public override void DisplayTimerText(string text)
        {
            _timerText.text = text;
        }

        /// <summary>
        /// Destroys the grenade object.
        /// </summary>
        public void DestroyGrenade()
        {
            if (_grenade != null)
            {
                Destroy(_grenade.gameObject);
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
            //    // if the character is a player, remove other players as potential targets and spawn selector
            //    if (character is PlayerBattleController && character.PotentialTargets.Count > 0)
            //    {
            //        PlayerBattleController player = character as PlayerBattleController;
            //
            //        for (int i = 0; i < player.PotentialTargets.Count; i++)
            //        {
            //            if (player.PotentialTargets[i].tag != "Enemy")
            //                player.PotentialTargets.Remove(player.PotentialTargets[i]);
            //        }
            //
            //        player.SpawnSelector(player.PotentialTargets[0].transform.position);
            //    }
            //}
            ///


        }

        #endregion
    }
}
