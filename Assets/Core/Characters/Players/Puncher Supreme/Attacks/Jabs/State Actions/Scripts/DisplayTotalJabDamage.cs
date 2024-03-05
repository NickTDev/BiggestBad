// Merle Roji

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Display the total damage done by the jabs.
    /// </summary>
    [CreateAssetMenu(fileName = "Display Total Jab Damage", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Display Total Jab Damage")]
    public class DisplayTotalJabComplete : StateActions
    {
        public override void Execute(StateManager states)
        {
            Jabs attack = (Jabs)states;

            // activate damage panel
            if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
            TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

            // display damage
            damageText.text = "Total Damage: " + attack.TotalDamage;
        }
    }
}