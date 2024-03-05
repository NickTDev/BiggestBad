// Merle Roji

using T02.Characters.InBattle;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

namespace T02.Characters
{
    /// <summary>
    /// A state machine parent class for all minigames.
    /// </summary>
    public abstract class MinigameStateMachine : StateManager
    {
        #region VARIABLES

        [SerializeField] private string _skillName = "Default Skill";
        [SerializeField, TextArea(15, 5)] private string _description = "This is a skill.";

        [Header("Base Parameters")]
        [SerializeField] protected LayerMask _characterLayer;
        [SerializeField] protected GameObject _hitVFX;
        [SerializeField] protected float _moveSpeed = 1f;
        [SerializeField] protected int _range = 0;
        [SerializeField, Min(0)] protected int _baseDamage = 1;
        [SerializeField] protected GameObject _damagePanel;
        [SerializeField] protected TextMeshProUGUI _readyText;
        protected int _damage = 0;
        [SerializeField] protected EnergyType _type;
        [SerializeField, Min(0)] protected int _baseEnergyValue = 1;
        [SerializeField, Min(0)] protected int _counteredEnergyValue = 1;
        protected int _adjustedEnergyValue;
        protected CharacterBattleController _character;
        protected Vector3 _initialPosition = Vector3.zero;
        protected Vector3 _initialTargetPosition = Vector3.zero;
        [SerializeField, Min(0)] protected float _maxTime = 0;
        protected float _currentTime = 0f;
        protected Camera _mainCam;
        [SerializeField, Min(0f)] private float _finishTime = 1.5f;
        [SerializeField] private Transform _followCamPrefab;
        private Transform _followCam;

        #endregion

        #region UNITY METHODS

        protected virtual void Awake()
        {
            _mainCam = Camera.main;
            _adjustedEnergyValue = _baseEnergyValue;
        }

        #endregion

        #region HELPERS

        public virtual CharacterBattleController GetCharacter()
        {
            return _character;
        }

        public string SkillName
        { get => _skillName; }

        public string Description
        { get => _description; }

        public int BaseDamage
        { get => _baseDamage; }

        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public EnergyType Type
        { get => _type; }

        public int BaseEnergyValue
        { get => _baseEnergyValue; }

        public int AdjustedEnergyValue
        {
            get => _adjustedEnergyValue;
            set => _adjustedEnergyValue = value;
        }

        public int CounteredEnergyValue
        { get => _counteredEnergyValue; }

        public Vector3 InitialPosition
        {
            get => _initialPosition;
            set => _initialPosition = value;
        }

        public Vector3 InitialTargetPosition
        {
            get => _initialTargetPosition;
            set => _initialTargetPosition = value;
        }

        public float MoveSpeed
        { get => _moveSpeed; }

        public float MaxTime
        { get => _maxTime; }

        public float CurrentTime
        {
            get => _currentTime;
            set => _currentTime = value;
        }

        public int Range
        { get => _range; }

        public GameObject DamagePanel
        { get => _damagePanel; }

        public float FinishTime
        { get => _finishTime; }

        /// <summary>
        /// Displays the current time from the timer.
        /// </summary>
        public virtual void DisplayTimerText(string text) { }

        /// <summary>
        /// Displays "GET READY!" over text.
        /// </summary>
        public void SetReadyText()
        {
            string ready = "GET READY!";

            if (_readyText != null)
            {
                if (_readyText.text != ready)
                    _readyText.text = ready;
            }
        }

        /// <summary>
        /// Turns off the ready text.
        /// </summary>
        public void TurnOffReadyText()
        {
            string ready = "";

            if (_readyText != null)
            {
                if (_readyText.text != ready)
                    _readyText.text = ready;
            }
        }

        /// <summary>
        /// Injects the character dependency.
        /// </summary>
        public void InjectCharacter(CharacterBattleController character)
        {
            _character = character;
        }

        /// <summary>
        /// Returns a canvas position equivalent to a world position from the viewport's perspective.
        /// Credit: YoungDeveloper
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 WorldToCanvasPosition(RectTransform canvas, Vector3 position)
        {
            // Vector position (percentage from 0 to 1) considering camera size.
            // For example (0,0) is lower left, middle is (0.5,0.5)
            Vector2 temp = _mainCam.WorldToViewportPoint(position);

            // Calculate position considering our percentage, using our canvas size
            // So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
            temp.x *= canvas.sizeDelta.x;
            temp.y *= canvas.sizeDelta.y;

            // The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
            // But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
            // We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot), 
            // returned value will still be correct.
            temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
            temp.y -= canvas.sizeDelta.y * canvas.pivot.y;

            return temp;
        }

        /// <summary>
        /// Checks for targets when targeting.
        /// </summary>
        public virtual void CheckForTargets(CharacterBattleController character) { }

        /// <summary>
        /// Returns a string from a given input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected string InputToString(PossibleInputs input)
        {
            string inputToString = "";
            PlayerBattleController player = _character as PlayerBattleController;

            switch (input)
            {
                case PossibleInputs.South:
                    {
                        if (player.IsPlayerUsingGamepad)
                            inputToString = "A";
                        else
                            inputToString = "S";

                        break;
                    }
                case PossibleInputs.East:
                    {
                        if (player.IsPlayerUsingGamepad)
                            inputToString = "B";
                        else
                            inputToString = "D";

                        break;
                    }
                case PossibleInputs.West:
                    {
                        if (player.IsPlayerUsingGamepad)
                            inputToString = "X";
                        else
                            inputToString = "A";

                        break;
                    }
                case PossibleInputs.North:
                    {
                        if (player.IsPlayerUsingGamepad)
                            inputToString = "Y";
                        else
                            inputToString = "W";

                        break;
                    }
            }

            return inputToString;
        }

        public Transform FollowCam
        { get => _followCam; }

        /// <summary>
        /// Spawns a Follow Cam object
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        public void SpawnFollowCam(Transform pointA, Transform pointB)
        {
            if (_character == null || _character.Target == null) return;

            if (_followCam == null) // if there is already a follow cam, dont do this
            {
                // get midway point
                Vector3 midwayPoint = (pointA.position + pointB.position) * 0.5f;
                _followCam = Instantiate(_followCamPrefab, midwayPoint, Quaternion.identity); // spawn camera at the midway point
                _character.TurnManager.SetCameraFollow(_followCam);
            }
        }

        /// <summary>
        /// Destroys the follow cam.
        /// </summary>
        public void DestroyFollowCam()
        {
            if (_followCam != null)
            {
                _character.TurnManager.SetCameraFollow(null);
                Destroy(_followCam.gameObject);
            }
        }

        /// <summary>
        /// Spawns a hit vfx.
        /// </summary>
        public void SpawnHitVFX(Transform target)
        {
            GameObject hit = Instantiate(_hitVFX, target.position, Quaternion.identity);
            if (hit != null) Destroy(hit.gameObject, 1f);
        }

        #endregion
    }
}
