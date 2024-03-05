// Merle Roji

// Merle Roji

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using T02.Characters;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using T02.TurnBasedSystem;
using T02.Characters.InBattle;
using System;

namespace T02.UI
{
    /// <summary>
    /// Handles logic of the dynamic Skill menu.
    /// </summary>
    public class SkillMenuUI : MonoBehaviour
    {
        #region CONTROLS

        [SerializeField] private PlayerControlsReference _controlsReference;
        private PlayerControls _controls;
        private InputAction _moveInput;
        private InputAction _selectInput;
        private InputAction _cancelInput;

        #endregion

        #region VARIABLES

        [SerializeField] private Button _skillButtonPrefab;
        [SerializeField] private Image _skillMenuPanel;
        [SerializeField] private GridLayoutGroup _skillLayout;
        [SerializeField] private TextMeshProUGUI _skillInfoName;
        [SerializeField] private TextMeshProUGUI _skillInfoEnergy;
        [SerializeField] private TextMeshProUGUI _skillInfoDescription;
        [SerializeField] private GameObject _energyAlertPanel;

        private Button[] _menuSelections;
        private int _menuIndex = 0;
        private Vector2 _movement;
        private Vector2 _initalSize;
        private bool _pressedMove = false;
        private TurnBasedManager _turnManager;
        private float _energyAlertTimer;
        private float _maxEnergyAlertTimer = 2f;

        private AudioSource _soundManager;
        [SerializeField] private AudioClip _menuSound;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _turnManager = GetComponentInParent<TurnBasedManager>();
            _menuIndex = 0;
            _energyAlertTimer = _maxEnergyAlertTimer;
            _initalSize = _skillMenuPanel.rectTransform.sizeDelta;
            InitControls();
            _soundManager = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        }

        private void Update()
        {
            CheckInput();
            //ToggleEnergyAlertOffAfterTime();
        }

        private void OnEnable()
        {
            _controls?.PlayerMenu.Enable();
        }

        private void OnDisable()
        {
            _controls?.PlayerMenu.Disable();

            // delete all old skills
            List<Button> previousSkills = new List<Button>();
            previousSkills.AddRange(GetComponentsInChildren<Button>());

            Debug.Log("Checking to see if any previous buttons...");

            if (previousSkills.Count > 0)
            {
                Debug.Log("Destroyed previous buttons");

                foreach (Button button in previousSkills)
                {
                    Destroy(button.gameObject);
                }
            }
        }

        #endregion

        #region INIT

        /// <summary>
        /// Adds the options to the menu array.
        /// </summary>
        public void InitMenuSelections()
        {
            // reset menu index to avoid bugs
            _menuIndex = 0;

            if (_turnManager.CurrentCharacterObject == null) return;

            List<Button> tempButtons = new List<Button>(); // make a temporary list
            
            // cycle through the attacks of the current character and dynamically load the ui
            for (int i = 0; i < _turnManager.CurrentCharacterAttackList.Length; i++)
            {
                // change the size of the panel
                RectTransform newButtonRect = _skillButtonPrefab.GetComponent<RectTransform>();
                _skillMenuPanel.rectTransform.sizeDelta = 
                    new Vector2(_initalSize.x, _initalSize.y + (i * (newButtonRect.rect.height + _skillLayout.spacing.y)));

                // spawn in button
                Button newButton = Instantiate(_skillButtonPrefab, _skillLayout.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = _turnManager.CurrentCharacterAttackList[i].SkillName;
                tempButtons.Add(newButton);
            }

            // selects the current skill
            _menuSelections = tempButtons.ToArray();
            SelectCurrentSkill(_menuIndex);

            // if the current skill needs more energy, show energy alert
            if (_turnManager.CurrentCharacterAttackList[_menuIndex].Type == EnergyType.Spend &&
                _turnManager.CurrentCharacter.Stats.CurrentEnergy < _turnManager.CurrentCharacterAttackList[_menuIndex].BaseEnergyValue)
            {
                ToggleEnergyAlertPanel(true);
            }
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        private void InitControls()
        {
            _controls = _controlsReference.Controls;

            _moveInput = _controls.PlayerMenu.Move;
            _moveInput.performed += ctx => _movement = ctx.ReadValue<Vector2>();

            _selectInput = _controls.PlayerMenu.Select;
            _cancelInput = _controls.PlayerMenu.Cancel;
        }

        #endregion

        #region MENU METHODS

        /// <summary>
        /// Checks input from the player.
        /// </summary>
        private void CheckInput()
        {
            if (_turnManager.CurrentCharacterObject == null) return;
            Vector2 movementNormalized = _movement.normalized;

            if (_pressedMove)
            {
                if (movementNormalized.y == 0f)
                {
                    _soundManager.PlayOneShot(_menuSound);
                    _pressedMove = false;
                }
            }

            if (!_pressedMove)
            {
                if (movementNormalized.y <= -1)
                {
                    if (_menuIndex < _menuSelections.Length - 1)
                    {
                        _menuIndex++;
                        SelectCurrentSkill(_menuIndex);
                    }
                    else
                    {
                        _menuIndex = 0;
                        SelectCurrentSkill(_menuIndex);
                    }

                    _pressedMove = true;
                }

                if (movementNormalized.y >= 1)
                {
                    if (_menuIndex > 0)
                    {
                        _menuIndex--;
                        SelectCurrentSkill(_menuIndex);
                    }
                    else
                    {
                        _menuIndex = _menuSelections.Length - 1;
                        SelectCurrentSkill(_menuIndex);
                    }

                    _pressedMove = true;
                }

                if (_turnManager.CurrentCharacterAttackList[_menuIndex].Type == EnergyType.Spend &&
                _turnManager.CurrentCharacter.Stats.CurrentEnergy < _turnManager.CurrentCharacterAttackList[_menuIndex].BaseEnergyValue)
                {
                    ToggleEnergyAlertPanel(true);
                }
                else
                {
                    ToggleEnergyAlertPanel(false);
                }
            }
        }

        #endregion

        #region HELPERS

        public bool PressedSelect
        { get => _selectInput.triggered; }

        public bool PressedCancel
        { get => _cancelInput.triggered; }

        public int MenuIndex
        { get => _menuIndex; }

        /// <summary>
        /// Toggles the Energy Alert Panel on or off.
        /// </summary>
        /// <param name="onOrOff"></param>
        public void ToggleEnergyAlertPanel(bool onOrOff)
        {
            if (onOrOff == true)
            {
                _energyAlertTimer = _maxEnergyAlertTimer;
            }

            _energyAlertPanel.SetActive(onOrOff);
        }

        /// <summary>
        /// Toggles the Energy Alert Panel off if left on for over a second.
        /// </summary>
        private void ToggleEnergyAlertOffAfterTime()
        {
            if (_energyAlertPanel.activeInHierarchy)
            {
                _energyAlertTimer -= Time.deltaTime;
                if (_energyAlertTimer <= 0f)
                {
                    _energyAlertTimer = 0f;
                    ToggleEnergyAlertPanel(false);
                }
            }
        }

        /// <summary>
        /// Selects the current Skill and displays all of its information.
        /// </summary>
        /// <param name="index"></param>
        private void SelectCurrentSkill(int index)
        {
            Button currentButton = _menuSelections[index];
            MinigameStateMachine currentSkill = _turnManager.CurrentCharacterAttackList[index];

            // highlight current skill
            currentButton.Select();

            // display skill information
            // name
            _skillInfoName.text = currentSkill.SkillName;

            // energy gain or cost
            if (currentSkill.Type == EnergyType.Gain)
                _skillInfoEnergy.text = "Gain: " + currentSkill.BaseEnergyValue + " EP (Min)";
            else if (currentSkill.Type == EnergyType.Spend)
                _skillInfoEnergy.text = "Cost: " + currentSkill.BaseEnergyValue + " EP (Max)";

            // description
            _skillInfoDescription.text = currentSkill.Description;
        }

        #endregion
    }
}
