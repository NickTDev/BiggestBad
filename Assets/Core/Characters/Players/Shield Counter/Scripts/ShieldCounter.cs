// Merle Roji

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Shield Counter attack.
    /// </summary>
    public class ShieldCounter : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Shield Parameters")]
        [SerializeField] private PlayerOrder _partyOrder;
        [SerializeField] private GameObject _shieldPrefab;
        private GameObject _shield = null;
        [SerializeField, Min(0.001f)] private float _shieldTime = 0.25f;
        [SerializeField, Min(0f)] private float _shieldCooldown = 0.25f;
        [SerializeField] private Slider _chargeSlider;
        [SerializeField] private Image _fill;
        [SerializeField] private TextMeshProUGUI _percentText;
        private Color _originalColor;
        private EnemyBattleController _engagedEnemy;
        [SerializeField, Min(1f)] private float _shieldDefenseMultiplier = 3f;
        private int _previousCurrentDefense = 0;
        [SerializeField] public AudioClip _shieldSound;

        private enum PlayerOrder
        {
            First = 0,
            Second = 1,
            Third = 2,
            Fourth = 3
        }

        #endregion

        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();

            CurrentState.OnEnterExecute(this);
            _chargeSlider.value = 0f;
            _originalColor = _fill.color;
        }

        protected override void Update()
        {
            base.Update();
            CheckIfEnemyFinishedAttacking();
        }

        private void OnValidate()
        {
            _chargeSlider.maxValue = _shieldCooldown;
        }

        private void OnDestroy()
        {
            _character.Stats.SetDefense(_character.Stats.BaseDefense);
        }

        #endregion

        #region SHIELD METHODS

        /// <summary>
        /// Spawns the shield object.
        /// </summary>
        public void SpawnShield()
        {
            if (!_shield)
            {
                Vector3 shieldPos = _character.transform.position;
                _shield = Instantiate(_shieldPrefab, shieldPos, Quaternion.identity);

                // multiply character defense during shielding
                _previousCurrentDefense = _character.Stats.CurrentDefense;
                _character.Stats.SetDefense(Mathf.RoundToInt(_previousCurrentDefense * _shieldDefenseMultiplier));
                Destroy(_shield, _shieldTime);
            }
        }

        public int PreviousCurrentDefense
        { get => _previousCurrentDefense; }

        /// <summary>
        /// Sets the color of the filled charge gauge to red.
        /// </summary>
        public void SetFillCooldownColor()
        {
            if (_fill.color != Color.red)
                _fill.color = Color.red;
        }

        /// <summary>
        /// Sets the color of the filled charge gauge to its original color.
        /// </summary>
        public void SetFillReadyColor()
        {
            if (_fill.color != _originalColor)
                _fill.color = _originalColor;
        }

        /// <summary>
        /// If the enemy finished attacking, destroy this game object.
        /// </summary>
        private void CheckIfEnemyFinishedAttacking()
        {
            if (_character != null)
            {
                if (_character.Stats.CurrentHP <= 0)
                {
                    Destroy(gameObject);
                }
            }

            if (_engagedEnemy != null)
            {
                if (!_engagedEnemy.IsAttacking)
                {
                    if (_character != null)
                    {
                        _character.ChangeAnimation("idle");
                        Destroy(gameObject);
                    }
                }
            }
        }

        #endregion

        #region HELPERS

        public GameObject Shield
        { get => _shield; }

        public float ShieldTime
        { get => _shieldTime; }

        public float ShieldCooldown
        { get => _shieldCooldown; }

        public Slider ChargeSlider
        { get => _chargeSlider; }

        public string PercentText
        {
            get => _percentText.text;
            set => _percentText.text = value;
        }

        public Color OriginalColor
        { get => _originalColor; }

        public EnemyBattleController EngagedEnemy
        {
            get => _engagedEnemy;
            set => _engagedEnemy = value;
        }

        public bool PressedActionButton
        {
            get
            {
                PlayerBattleController player = _character as PlayerBattleController;

                switch(_partyOrder)
                {
                    case PlayerOrder.First:
                        return player.Player1ActionPressed;

                    case PlayerOrder.Second:
                        return player.Player2ActionPressed;

                    case PlayerOrder.Third:
                        return player.Player3ActionPressed;

                    case PlayerOrder.Fourth:
                        return player.Player4ActionPressed;
                }

                return false;
            }
        }

        #endregion
    }
}