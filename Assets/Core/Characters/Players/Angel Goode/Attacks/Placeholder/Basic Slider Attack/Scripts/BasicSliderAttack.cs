// Merle Roji

using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    public class BasicSliderAttack : MinigameStateMachine
    {
        #region VARIABLES

        [SerializeField] private Slider _attackSlider;
        [SerializeField] private GameObject _placeholderDamagePanel;
        [SerializeField] private float _sliderSpeed = 1f;

        #endregion

        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();

            _attackSlider.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            CurrentState.OnEnterExecute(this);
        }

        #endregion

        #region HELPERS

        public Slider AttackSlider
        { get => _attackSlider; }

        public GameObject PlaceholderDamagePanel
        { get => _placeholderDamagePanel; }

        public float SliderSpeed
        { get => _sliderSpeed; }

        #endregion
    }
}
