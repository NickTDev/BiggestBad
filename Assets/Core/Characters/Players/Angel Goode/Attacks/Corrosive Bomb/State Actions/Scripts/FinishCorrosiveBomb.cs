// Merle Roji

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Calculate the damage and apply the status of the corrosive bomb.
    /// </summary>
    [CreateAssetMenu(fileName = "Finish Corrosive Bomb", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Finish Corrosive Bomb")]
    public class FinishCorrosiveBomb : StateActions
    {
        public override void Execute(StateManager states)
        {
            CorrosiveBomb attack = (CorrosiveBomb)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            if (attack.SuccessCount > 0) // check if there are successes (safetey check)
            {
                // show damage panel
                if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
                TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

                // calculate damage
                int baseDamage = attack.BaseDamage * attack.SuccessCount;
                int attackFromAttacker = player.Stats.CurrentAttack;
                int damage = player.Target.Stats.TakeDamage(baseDamage, attackFromAttacker);
                attack.SpawnHitVFX(player.Target.transform);
                Debug.Log(damage);

                // display damage
                damageText.text = "Damage: " + damage;

                // apply vulnerability
                player.Target.AddNewEffect(new VulnerableEffect(player.Target, attack.SuccessCount));
                player.SoundManager.PlayOneShot(attack._corrosiveSound);
            }
        }
    }
}
