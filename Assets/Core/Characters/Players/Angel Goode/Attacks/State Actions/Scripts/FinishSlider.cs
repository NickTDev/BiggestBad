// Merle Roji

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    [CreateAssetMenu(fileName = "Finish Slider", menuName = "Behavior Tree Pattern/New Action/Placeholder/Finish Slider")]
    public class FinishSlider : StateActions
    {
        public override void Execute(StateManager states)
        {
            GrenadeToss attack = (GrenadeToss)states;
            CharacterBattleController character = attack.GetCharacter();

            if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
            TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

            int baseDamage = attack.Damage;
            int attackFromAttacker = character.Stats.CurrentAttack;
            int damage = character.Target.Stats.TakeDamage(baseDamage, attackFromAttacker);
            attack.SpawnHitVFX(character.Target.transform);
            Debug.Log(damage);

            damageText.text = "Damage: " + damage;
        }
    }
}
