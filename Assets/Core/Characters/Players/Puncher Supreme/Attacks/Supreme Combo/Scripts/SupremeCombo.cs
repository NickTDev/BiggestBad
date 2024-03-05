// Merle Roji

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Puncher Supreme's multihit combo skill.
    /// </summary>
    public class SupremeCombo : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Supreme Combo Parameters")]
        [SerializeField] private Slider _attackSlider;
        [SerializeField] private ButtonIcon _buttonIcon;
        [SerializeField] private TextMeshProUGUI _comboText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _totalHitsText;
        [SerializeField, Range(0.1f, 0.9f)] private float _logScale = 0.5f;
        private int _requiredInput;
        private float _maxLogTime;
        private int _totalDamage;
        private int _totalHits;
        [SerializeField] public AudioClip _comboSound;

        #endregion

        #region UNITY METHODS

        private void OnValidate()
        {
            _attackSlider.maxValue = _maxTime;
            _attackSlider.value = _attackSlider.maxValue;
        }

        #endregion

        #region SUPREME COMBO METHODS

        /// <summary>
        /// Generates an input.
        /// </summary>
        public void GenerateInput()
        {
            // default
            _comboText.text = "";
            _requiredInput = -1;

            PossibleInputs randomInput = (PossibleInputs)Random.Range(0, 4); // generate random input

            // sets the correct button
            _buttonIcon.Input = randomInput;
            if (!_buttonIcon.gameObject.activeInHierarchy) _buttonIcon.gameObject.SetActive(true);

            _requiredInput = (int)randomInput; // set required input
        }

        // updates the ui to total hits
        public void UpdateTotalHits()
        {
            _totalHitsText.text = "Total Hits: " + _totalHits;
        }

        #endregion

        #region HELPERS

        public Slider AttackSlider
        { get => _attackSlider; }

        public float MaxLogTime
        {
            get => _maxLogTime;
            set => _maxLogTime = value;
        }

        public int RequiredInput
        { get => _requiredInput; }

        public int TotalDamage
        {
            get => _totalDamage;
            set => _totalDamage = value;
        }

        public int TotalHits
        {
            get => _totalHits;
            set => _totalHits = value;
        }

        /// <summary>
        /// Return new time based on a Logarithmic scale.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public float LogarithmicTime()
        {
            // y = (max time) * scale ^ (total hits)
            float newTime = _maxTime * (Mathf.Pow(_logScale, _totalHits));
            return newTime;
        }

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

        #endregion
    }
}