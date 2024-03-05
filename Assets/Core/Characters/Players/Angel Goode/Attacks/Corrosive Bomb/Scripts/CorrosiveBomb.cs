// Merle Roji

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Angel Goode's Corrosive Bomb skill.
    /// </summary>
    public class CorrosiveBomb : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Corrosive Bomb Parameters")]
        [SerializeField] private GameObject _bombPrefab;
        private GameObject _bomb = null;
        [SerializeField] private Slider _firstSlider;
        [SerializeField] private Slider _secondSlider;
        [SerializeField] private Slider _thirdSlider;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _successText;
        private Slider[] _allSliders;
        private int _sliderIndex;
        private int _successCount;
        [SerializeField] public AudioClip _throwSound;
        [SerializeField] public AudioClip _corrosiveSound;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            Slider[] sliders = { _firstSlider, _secondSlider, _thirdSlider };
            _allSliders = sliders;
            _sliderIndex = 0;
            _successCount = 0;
        }

        private void OnValidate()
        {
            _firstSlider.maxValue = _maxTime;
            _secondSlider.maxValue = _maxTime;
            _thirdSlider.maxValue = _maxTime;
        }

        #endregion

        #region HELPERS

        public Slider CurrentSlider
        { get => _allSliders[_sliderIndex]; }

        public int SliderIndex
        {
            get => _sliderIndex;
            set => _sliderIndex = value;
        }

        public int SuccessCount
        {
            get => _successCount;
            set => _successCount = value;
        }

        public int AmountOfSliders
        { get => _allSliders.Length; }

        public bool DidBombReachTarget
        {
            get
            {
                if (_bomb == null) return false; // return if no grenade

                Vector3 grenadePos = _bomb.transform.position; // store grenade position
                float targetDiameter = _character.Target.GetComponent<CapsuleCollider>().radius * 2; // store target radius
                Vector3 targetPos = _character.Target.transform.position;
                float distance = Vector3.Distance(grenadePos, targetPos); // calc distance

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

        public override void DisplayTimerText(string text)
        {
            _timerText.text = text;
        }

        public void UpdateMaxTime()
        {
            _maxTime = _maxTime / (1 + (_sliderIndex * 0.5f));
            CurrentSlider.maxValue = _maxTime;
            //CurrentSlider.value = _maxTime;
            _currentTime = _maxTime;
        }

        public void UpdateSuccessCount()
        {
            _successText.text = "Successes: " + _successCount;
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

        #endregion
    }
}