// Merle Roji

using UnityEngine;
using UnityEngine.UI;
using T02.Characters;
using TMPro;
using T02.TurnBasedSystem;

namespace T02.UI
{
    /// <summary>
    /// Displays the character HP and Energy.
    /// </summary>
    public abstract class CharacterResourceUI : MonoBehaviour
    {
        protected TurnBasedManager _turnManager;

        [SerializeField] protected TextMeshProUGUI _nameText;
        [SerializeField] protected TextMeshProUGUI _hpNumberText;
        [SerializeField] protected TextMeshProUGUI _epNumberText;
        [SerializeField] protected TextMeshProUGUI _moveNumberText;
        [SerializeField] protected Slider _hpSlider;
        [SerializeField] protected Slider _epSlider;
        [SerializeField] protected Slider _moveSlider;

        protected virtual void Awake ()
        {
            Init();
        }

        protected virtual void Update ()
        {
            TickUI();
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        protected virtual void Init()
        {
            if (IsUINull())
            {
                Debug.LogError("Error: Missing Character UI Elements!");
                return;
            }

            _turnManager = GetComponentInParent<TurnBasedManager>();
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        protected virtual void TickUI()
        {
            
        }

        /// <summary>
        /// Checks to see if any UI elements are null.
        /// </summary>
        /// <returns></returns>
        protected bool IsUINull()
        {
            if (!_nameText || !_hpSlider || !_hpNumberText || !_epSlider || !_epNumberText || !_moveSlider || !_moveNumberText)
                return true;

            return false;
        }
    }
}
