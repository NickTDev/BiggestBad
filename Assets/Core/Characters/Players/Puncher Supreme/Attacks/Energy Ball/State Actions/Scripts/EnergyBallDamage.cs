// Merle Roji

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    [CreateAssetMenu(fileName = "Energy Ball Damage", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Energy Ball Damage")]
    public class EnergyBallDamage : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnergyBall attack = (EnergyBall)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
            TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

            int baseDamage = attack.BaseDamage + Mathf.RoundToInt(attack.AttackSlider.value) * 4;
            int attackFromAttacker = player.Stats.CurrentAttack;
            int damage = player.Target.Stats.TakeDamage(baseDamage, attackFromAttacker);
            attack.SpawnHitVFX(player.Target.transform);
            Debug.Log(damage);

            damageText.text = "Damage: " + damage;
        }
    }
}
