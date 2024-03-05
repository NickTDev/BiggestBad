// Merle Roji

using UnityEngine;
using TMPro;

namespace T02.Characters.InBattle
{
    [CreateAssetMenu(fileName = "Damage Target", menuName = "Behavior Tree Pattern/New Action/Common/Character/Damage Target")]
    public class DamageTarget : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            int baseDamage = attack.Damage;
            int attackFromAttacker = character.Stats.CurrentAttack;
            int damage = character.Target.Stats.TakeDamage(baseDamage, attackFromAttacker);
            attack.SpawnHitVFX(character.Target.transform);

            character.SoundManager.PlayOneShot(character.HurtSound);

            // activate damage panel
            if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
            TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

            // display damage
            damageText.text = "Damage: " + damage;

            Debug.Log(damage);
        }
    }
}
