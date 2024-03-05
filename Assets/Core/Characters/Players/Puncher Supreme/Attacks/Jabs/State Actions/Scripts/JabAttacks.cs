// Merle Roji

using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// The input for the jab attacks.
    /// </summary>
    [CreateAssetMenu(fileName = "Jab Attacks", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Jab Attacks")]
    public class JabAttacks : StateActions
    {
        public override void Execute(StateManager states)
        {
            Jabs attack = (Jabs)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            // create a temporary integer that does not match any button press.
            int currentPress = -1;

            // depending on the attack, the current press changes.
            if (player.Player1ActionPressed)
                currentPress = 0;
            else if (player.Player2ActionPressed)
                currentPress = 1;
            else if (player.Player3ActionPressed)
                currentPress = 2;
            else if (player.Player4ActionPressed)
                currentPress = 3;

            // if there are attacks left to do, check for attacks.
            // if the last attack is hit, deal double damage.
            if(attack.ComboString.Count > 0)
            {
                // check current press against the combo string.
                if (currentPress == attack.ComboString[0])
                {
                    // activate damage panel
                    if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
                    TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

                    // deal damage
                    int baseDamage;
                    int attackFromAttacker;
                    int energyGained;

                    // animation
                    if (currentPress == 0)
                        player.ChangeAnimation("leftJab");
                    else if (currentPress == 1)
                        player.ChangeAnimation("leftJab");
                    else if (currentPress == 2)
                        player.ChangeAnimation("rightJab");
                    else if (currentPress == 3)
                        player.ChangeAnimation("rightJab");

                    if (attack.ComboString.Count > 1)
                    {
                        baseDamage = attack.BaseDamage;
                        attackFromAttacker = Mathf.RoundToInt(player.Stats.CurrentAttack * 0.5f);
                        energyGained = attack.BaseEnergyValue;
                    }
                    else
                    {
                        baseDamage = (int)(attack.BaseDamage * attack.FinalAttackMultiplier);
                        attackFromAttacker = (int)(player.Stats.CurrentAttack * 0.5f * attack.FinalAttackMultiplier);
                        energyGained = (int)(attack.BaseEnergyValue * attack.FinalAttackMultiplier);
                    }

                    int damage = player.Target.Stats.TakeDamage(baseDamage, attackFromAttacker);
                    attack.SpawnHitVFX(player.Target.transform);
                    player.Target.SoundManager.PlayOneShot(player.Target.HurtSound);
                    attack.TotalDamage += damage;
                    Debug.Log(damage);

                    // display damage
                    damageText.text = "Damage: " + damage;

                    // gain energy
                    player.Stats.GainEnergy(energyGained);

                    // remove the attack from the combo string
                    attack.ComboButtons[attack.AttackIndex].ButtonCleared(true);
                    attack.ComboString.Remove(attack.ComboString[0]);
                    attack.AttackIndex++;
                }
            }
        }
    }
}