// Merle Roji

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Puncher Supreme's Energy Ball skill.
    /// </summary>
    public class EnergyBall : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Energy Ball Parameters")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Slider _attackSlider;
        [SerializeField] private TextMeshProUGUI _comboText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField, Min(0.01f)] private float _inputBufferTime = 0.5f;
        private List<Vector2> _requiredMoveString = new List<Vector2>();
        private List<Vector2> _recordedMoveInputs = new List<Vector2>();
        private int _requiredInput = -1;
        private float _joystickTimer;
        private PlayerBattleController _player;
        private bool _pressedDown = false;
        private GameObject _energyBallObj = null;
        [SerializeField] public AudioClip _energyBallSound;

        private enum LeftOrRightInputs
        {
            Right = 0,
            Left = 1
        }

        #endregion

        #region UNITY METHODS

        private void OnValidate()
        {
            _attackSlider.maxValue = _maxTime;
            _attackSlider.value = _attackSlider.maxValue;
        }

        #endregion

        #region ENERGY BALL METHODS

        /// <summary>
        /// Generates a combo that the player must perform.
        /// </summary>
        public void GenerateCombo()
        {
            // default
            //_comboText.text = "";
            _requiredInput = -1;
            _player = _character as PlayerBattleController;
            _joystickTimer = _inputBufferTime;
            _pressedDown = false;
            _requiredMoveString.Clear();
            _recordedMoveInputs.Clear();

            // add movement to required input
            //_comboText.text = "v > ";
            Vector2 downInput = new Vector2(0.0f, -1.0f);
            //Vector2 downRightInput = new Vector2(0.5f, -0.5f);
            Vector2 rightInput = new Vector2(1.0f, 0.0f);
            Vector2[] inputs = { downInput, /*downRightInput,*/ rightInput };

            _requiredMoveString.AddRange(inputs);
            _requiredInput = (int)PossibleInputs.East;
        }

        /// <summary>
        /// Records movement inputs.
        /// </summary>
        public void RecordMovements()
        {
            if (_character == null) return;

            if (_pressedDown) // start timer if the joystick was pressed down
            {
                if (_joystickTimer > 0f)
                {
                    _joystickTimer -= Time.deltaTime;
                    Vector2 roundedJoystickInputs = new Vector2(
                        (float)System.Math.Round(_player.LeftStickMovement.x, 1),
                        (float)System.Math.Round(_player.LeftStickMovement.y, 1)
                        );
                    Vector2 roundedDPadInputs = new Vector2(
                        (float)System.Math.Round(_player.DPadMovement.x, 1),
                        (float)System.Math.Round(_player.DPadMovement.y, 1)
                        );

                    _recordedMoveInputs.Add(roundedJoystickInputs); // record all joystick inputs
                    _recordedMoveInputs.Add(roundedDPadInputs); // record all dpad inputs
                }
                else
                {
                    // clear recorded inputs
                    _joystickTimer = _inputBufferTime;
                    _pressedDown = false;
                }
            }
        }

        /// <summary>
        /// Check if the player pressed down.
        /// </summary>
        public void CheckIfDownPressed()
        {
            if (_character == null) return;

            if (!_pressedDown)
            {
                if ((System.Math.Round(_player.LeftStickMovement.x, 1) == 0.0f && 
                     System.Math.Round(_player.LeftStickMovement.y, 1) <= -1.0f) ||
                    (System.Math.Round(_player.DPadMovement.x, 1) == 0.0f &&
                     System.Math.Round(_player.DPadMovement.y, 1) <= -1.0f))
                {
                    _recordedMoveInputs.Clear();
                    _pressedDown = true;
                }
            }
        }

        #endregion

        #region HELPERS

        public Slider AttackSlider
        { get => _attackSlider; }

        public List<Vector2> RequiredMoveString
        { get => _requiredMoveString; }

        public List<Vector2> RecordedMoveInputs
        { get => _recordedMoveInputs; }

        public int RequiredInput
        { get => _requiredInput; }

        public override void DisplayTimerText(string text)
        {
            _timerText.text = text;
        }

        public override void CheckForTargets(CharacterBattleController character)
        {
            /// COMPLETE PLACEHOLDER - REPLACE THIS WITH NEW TARGETING SYSTEM
            //// get array of character colliders
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

        /// <summary>
        /// Spawns the energy ball object.
        /// </summary>
        public void SpawnEnergyBall()
        {
            if (_energyBallObj == null)
            {
                _character.AttackPivot.LookAt(_character.Target.transform);
                _energyBallObj = Instantiate(_projectilePrefab, _character.AttackPivot);
                Rigidbody energyBallRb = _energyBallObj.GetComponent<Rigidbody>();
                energyBallRb.velocity = _character.AttackPivot.forward * _moveSpeed;
            }
        }

        /// <summary>
        /// Destroys an existing energy ball.
        /// </summary>
        public void DestroyProjectile()
        {
            if (_energyBallObj != null)
            {
                Destroy(_energyBallObj.gameObject);
            }
        }

        public bool DidProjectileReachTarget
        {
            get
            {
                if (_energyBallObj == null) return false; // return if no ball

                Vector3 energyBallPos = _energyBallObj.transform.position; // store grenade position
                float targetDiameter = _character.Target.GetComponent<CapsuleCollider>().radius * 2; // store target radius
                Vector3 targetPos = _character.Target.transform.position;
                float distance = Vector3.Distance(energyBallPos, targetPos); // calc distance

                if (distance <= targetDiameter) return true;
                else return false;
            }
        }

        #endregion
    }
}