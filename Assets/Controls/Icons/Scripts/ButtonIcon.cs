// Merle Roji

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.DualShock;

namespace T02
{
    public class ButtonIcon : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private Image _buttonImage;
        [SerializeField] private PossibleInputs _input = PossibleInputs.South;
        [SerializeField] private bool _disableSprite = false;
        [SerializeField] private Sprite[] _keyButtons;
        [SerializeField] private Sprite[] _xboxButtons;
        [SerializeField] private Sprite[] _psButtons;

        #endregion

        #region UNITY METHODS

        private void OnEnable()
        {
            _buttonImage.sprite = null;
        }

        private void Update()
        {
            MatchIconToController();
        }

        private void OnValidate()
        {
            MatchIconToController();
            ButtonCleared(_disableSprite);
        }

        #endregion

        #region HELPERS

        public void MatchIconToController()
        {
            if ((int)_input < (int)PossibleInputs.Select)
            {
                if (Gamepad.current is XInputController)
                {
                    _buttonImage.sprite = _xboxButtons[(int)_input];
                }
                else if (Gamepad.current is DualShockGamepad)
                {
                    _buttonImage.sprite = _psButtons[(int)_input];
                }
                else if (Gamepad.current == null)
                {
                    _buttonImage.sprite = _keyButtons[(int)_input];
                }
            }

            if (_input == PossibleInputs.Select)
            {
                if (Gamepad.current is XInputController)
                {
                    _buttonImage.sprite = _xboxButtons[(int)PossibleInputs.South];
                }
                else if (Gamepad.current is DualShockGamepad)
                {
                    _buttonImage.sprite = _psButtons[(int)PossibleInputs.South];
                }
                else if (Gamepad.current == null)
                {
                    _buttonImage.sprite = _keyButtons[(int)PossibleInputs.Select];
                }
            }

            if (_input == PossibleInputs.Back)
            {
                if (Gamepad.current is XInputController)
                {
                    _buttonImage.sprite = _xboxButtons[(int)PossibleInputs.East];
                }
                else if (Gamepad.current is DualShockGamepad)
                {
                    _buttonImage.sprite = _psButtons[(int)PossibleInputs.East];
                }
                else if (Gamepad.current == null)
                {
                    _buttonImage.sprite = _keyButtons[(int)PossibleInputs.Back];
                }
            }

            if ((int)_input >= (int)PossibleInputs.RBumper)
            {
                if (Gamepad.current is XInputController)
                {
                    _buttonImage.sprite = _xboxButtons[(int)_input - 2];
                }
                else if (Gamepad.current is DualShockGamepad)
                {
                    _buttonImage.sprite = _psButtons[(int)_input - 2];
                }
                else if (Gamepad.current == null)
                {
                    _buttonImage.sprite = _keyButtons[(int)_input];
                }
            }
        }

        public void ButtonCleared(bool clear = false)
        {
            if (clear)
            {
                _buttonImage.color = new Color(1f, 1f, 1f, 0.3f);
            }
            else
            {
                _buttonImage.color = Color.white;
            }
        }

        public PossibleInputs Input
        { 
            get => _input;
            set => _input = value;
        }

        #endregion
    }
}