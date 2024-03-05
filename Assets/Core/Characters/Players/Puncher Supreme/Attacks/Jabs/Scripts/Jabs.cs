// Merle Roji

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Puncher Supreme's basic attack.
    /// </summary>
    public class Jabs : MinigameStateMachine
    {
        #region VARIABLES

        [Header("Jabs Parameters")]
        [SerializeField, Min(1)] private int _maxInputs = 3;
        [SerializeField, Min(0f)] private float _finalAttackMultiplier = 2f;
        [SerializeField] private Slider _attackSlider;
        [SerializeField] private ButtonIcon[] _comboButtons;
        [SerializeField] private TextMeshProUGUI _comboText;
        [SerializeField] private TextMeshProUGUI _timerText;
        private List<int> _comboString = new List<int>();
        private int _attackIndex = 0;
        private int _totalDamage;
        [SerializeField] public AudioClip _jabSound;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            CurrentState.OnEnterExecute(this);
        }

        private void OnValidate()
        {
            _attackSlider.maxValue = _maxTime;
            _attackSlider.value = _attackSlider.maxValue;
        }

        #endregion

        #region JABS METHODS

        /// <summary>
        /// Generates a combo that the player must perform.
        /// </summary>
        public void GenerateCombo()
        {
            _comboText.text = "";
            _comboString.Clear();

            for (int i = 0; i < _maxInputs; i++)
            {
                PossibleInputs randomInput = (PossibleInputs)Random.Range(0, 4); // generate random input

                // sets the correct button
                _comboButtons[i].Input = randomInput;
                _comboButtons[i].gameObject.SetActive(true);

                _comboString.Add((int)randomInput);
            }
        }

        #endregion

        #region HELPERS

        public Slider AttackSlider
        { get => _attackSlider; }

        public ButtonIcon[] ComboButtons
        { get => _comboButtons; }

        public List<int> ComboString
        { get => _comboString; }

        public int AttackIndex
        {
            get => _attackIndex;
            set => _attackIndex = value;
        }

        public float FinalAttackMultiplier
        { get => _finalAttackMultiplier; }

        public int TotalDamage
        {
            get => _totalDamage;
            set => _totalDamage = value;
        }

        public bool IsComboComplete
        { get => _comboString.Count <= 0; }

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