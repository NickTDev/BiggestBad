// Merle Roji

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Angel Goode's Goo Bomb skill.
    /// </summary>
    public class GooBomb : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Goo Bomb Parameters")]
        [SerializeField] private GameObject _bombPrefab;
        private GameObject _bomb = null;
        [SerializeField] private GameObject _explosionPrefab;
        private GameObject _explosion = null;
        [SerializeField] private Slider _bombSlider;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Image _colorPanel;
        [SerializeField, Min(0f)] private float _sliderSpeed = 1f;
        [SerializeField, Min(0f)] private float _colorswapRandomTimeRange = 0.25f;
        [SerializeField, Min(0f)] private float _colorswapTimeMultiplier = 0.25f;
        private bool _buttonHeld = false;
        private float _colorswapTimer = 0f;
        private int _amountOfTurns = 1;
        [SerializeField] public AudioClip _throwSound;
        [SerializeField] public AudioClip _gooSound;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            int randInt = Random.Range(0, 2);
            if (randInt == 0)
            {
                _colorPanel.color = Color.green;
            }
            else if (randInt == 1)
            {
                _colorPanel.color = Color.red;
            }

            ResetColorswapTimer();
        }

        #endregion

        #region GOO BOMB METHODS

        /// <summary>
        /// Toggles the panel color from red to green and vice versa.
        /// </summary>
        public void TogglePanelColor()
        {
            if (_colorPanel.color == Color.red)
                _colorPanel.color = Color.green;
            else if (_colorPanel.color == Color.green)
                _colorPanel.color = Color.red;
        }

        #endregion

        #region HELPERS

        public Slider BombSlider
        { get => _bombSlider; }

        public Image ColorPanel
        { get => _colorPanel; }

        public bool ButtonHeld
        {
            get => _buttonHeld;
            set => _buttonHeld = value;
        }

        public float SliderSpeed
        { get => _sliderSpeed; }

        public float ColorswapTimer
        {
            get => _colorswapTimer;
            set => _colorswapTimer = value;
        }

        public int AmountOfTurns
        {
            get => _amountOfTurns;
            set => _amountOfTurns = value;
        }

        public bool DidBombReachTarget
        {
            get
            {
                if (_bomb == null) return false; // return if no grenade

                Vector3 bombPos = _bomb.transform.position; // store grenade position
                float targetDiameter = _character.Target.GetComponent<CapsuleCollider>().radius * 2; // store target radius
                Vector3 targetPos = _character.Target.transform.position;
                float distance = Vector3.Distance(bombPos, targetPos); // calc distance

                if (distance <= targetDiameter) return true;
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
                _explosion.GetComponent<GooBombExplosion>().InjectDamage(this, Damage, _character.Stats.CurrentAttack, _amountOfTurns);
                DestroyBomb();
            }
        }

        public bool DidExplosionEnd
        {
            get
            {
                return _explosion == null;
            }
        }

        public override void DisplayTimerText(string text)
        {
            _timerText.text = text;
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

        public void ResetColorswapTimer()
        {
            float randAlter = Random.Range(-_colorswapRandomTimeRange, _colorswapRandomTimeRange);
            _colorswapTimer = (_maxTime * _colorswapTimeMultiplier) + randAlter;
        }

        #endregion
    }
}