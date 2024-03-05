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
    /// Handles logic of the base Battle Menu.
    /// </summary>
    public class BattleMenuUI : MonoBehaviour
    {
        #region CONTROLS

        [SerializeField] private PlayerControlsReference _controlsReference;
        private PlayerControls _controls;
        private InputAction _moveInput;
        private InputAction _selectInput;
        private InputAction _cancelInput;
        private InputAction _turnOffTutorialInput;

        #endregion

        #region VARIABLES

        private Button[] _menuSelections;
        private int _menuIndex = 0;
        private int _lastMenuIndex = 0;
        private Vector2 _movement;
        private bool _pressedMove = false;
        private TurnBasedManager _turnManager;
        private AudioSource _soundManager;
        [SerializeField] private AudioClip _menuSound;
        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private GameObject[] _tutorialTexts;
        [SerializeField, Min(0)] private int _tutorialIndex = 0;

        private enum Menu
        {
            Skills = 0,
            Move,
            EndTurn
        }

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            _turnManager = GetComponentInParent<TurnBasedManager>();
            _lastMenuIndex = 0;
            _tutorialIndex = 0;
            _soundManager = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        }

        private void Awake()
        {
            InitControls();
        }

        private void Update()
        {
            CheckInput();

            if (_turnOffTutorialInput.triggered && _tutorialIndex < _tutorialTexts.Length - 1)
            {
                _tutorialIndex = 50;
                ChangeTutorialDisplay();
            }
        }

        private void OnEnable()
        {
            _menuIndex = _lastMenuIndex;
            _controls?.PlayerMenu.Enable();
        }

        private void OnDisable()
        {
            _lastMenuIndex = _menuIndex;
            _controls?.PlayerMenu.Disable();
        }

        private void OnValidate()
        {
            ChangeTutorialDisplay();
        }

        #endregion

        #region INIT

        /// <summary>
        /// Adds the options to the menu array.
        /// </summary>
        public void InitMenuSelections()
        {
            List<Button> tempButtons = new List<Button>();
            tempButtons.AddRange(GetComponentsInChildren<Button>());
            _menuSelections = tempButtons.ToArray();
            tempButtons.Clear();
            _menuIndex = 0;
            _menuSelections[_menuIndex].Select();
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
            _turnOffTutorialInput = _controls.PlayerMenu.TurnOffTutorial;
        }

        #endregion

        #region MENU METHODS

        /// <summary>
        /// Checks input from the player.
        /// </summary>
        private void CheckInput()
        {
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
                        _menuSelections[_menuIndex].Select();
                    }
                    else
                    {
                        _menuIndex = 0;
                        _menuSelections[_menuIndex].Select();
                    }

                    _pressedMove = true;
                }

                if (movementNormalized.y >= 1)
                {
                    if (_menuIndex > 0)
                    {
                        _menuIndex--;
                        _menuSelections[_menuIndex].Select();
                    }
                    else
                    {
                        _menuIndex = _menuSelections.Length - 1;
                        _menuSelections[_menuIndex].Select();
                    }

                    _pressedMove = true;
                }
            }
        }

        /// <summary>
        /// Changes which tutorial is displayed.
        /// </summary>
        public void ChangeTutorialDisplay()
        {
            if (_tutorialTexts.Length > 0)
            {
                if (_tutorialIndex <= _tutorialTexts.Length - 1)
                {
                    if (!_tutorialPanel.activeInHierarchy) _tutorialPanel.SetActive(true);

                    foreach (var tut in _tutorialTexts)
                    {
                        if (tut != _tutorialTexts[_tutorialIndex])
                            tut.SetActive(false);
                    }

                    if (!_tutorialTexts[_tutorialIndex].activeInHierarchy)
                        _tutorialTexts[_tutorialIndex].SetActive(true);
                }
                else if (_tutorialIndex > _tutorialTexts.Length - 1)
                {
                    foreach (var tut in _tutorialTexts)
                    {
                        tut.SetActive(false);
                    }

                    _tutorialPanel.SetActive(false);
                }
            }
        }
        
        #endregion

        #region HELPERS

        public bool PressedSelect
        { get => _selectInput.triggered; }

        public bool PressedCancel
        { get => _cancelInput.triggered; }

        public bool PressedSkillOption
        { get => (PressedSelect && _menuIndex == (int)Menu.Skills); }

        public bool PressedMoveOption
        { get => (PressedSelect && _menuIndex == (int)Menu.Move); }

        public bool PressedEndTurnOption
        { get => (PressedSelect && _menuIndex == (int)Menu.EndTurn); }

        public int TutorialIndex
        {
            get => _tutorialIndex;
            set => _tutorialIndex = value;
        }

        #endregion
    }
}