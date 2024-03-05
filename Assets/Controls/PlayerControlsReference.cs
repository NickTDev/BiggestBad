// Merle Roji

using UnityEngine;
using UnityEngine.InputSystem;

namespace T02
{
    /// <summary>
    /// Stores a reference to the controls.
    /// </summary>
    [CreateAssetMenu(fileName = "Player Controls", menuName = "Controls/New Player Controls")]
    public class PlayerControlsReference : ScriptableObject
    {
        private PlayerControls _controls;
        public PlayerControls Controls
        { get => _controls; }

        private void OnEnable()
        {
            _controls = new PlayerControls();
            InputSystem.pollingFrequency = 180f;
        }

        private void OnDisable()
        {
            _controls.Disable();
        }
    }
}