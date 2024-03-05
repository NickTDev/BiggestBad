// Merle Roji

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Display the total damage done by the supreme combo.
    /// </summary>
    [CreateAssetMenu(fileName = "Display Supreme Damage", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Display Supreme Damage")]
    public class DisplayTotalSupremeDamage : StateActions
    {
        public override void Execute(StateManager states)
        {
            SupremeCombo attack = (SupremeCombo)states;

            // activate damage panel
            if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
            TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

            // display damage
            damageText.text = "Total Damage: " + attack.TotalDamage;
        }
    }
}