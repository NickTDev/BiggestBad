// Merle Roji

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// The input for the supreme attacks.
    /// </summary>
    [CreateAssetMenu(fileName = "Input Supreme Attacks", menuName = "Behavior Tree Pattern/New Condition/Specific/Puncher Supreme/Input Supreme Attacks")]
    public class InputSupremeAttacks : Condition
    {
        private void OnValidate()
        {
            Description = "Inputted correct input?";
        }

        public override bool CheckCondition(StateManager state)
        {
            SupremeCombo attack = (SupremeCombo)state;
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

            // check current press against the combo string.
            if (currentPress == attack.RequiredInput)
            {
                // activate damage panel
                if (!attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(true); }
                TextMeshProUGUI damageText = attack.DamagePanel.GetComponentInChildren<TextMeshProUGUI>();

                // animation
                int randomAnim = Random.Range(0, 3);
                if (randomAnim == 0)
                    player.ChangeAnimation("leftJab");
                else if (randomAnim == 1)
                    player.ChangeAnimation("rightJab");
                else if (randomAnim == 2)
                    player.ChangeAnimation("dualPalms");

                // deal damage
                int baseDamage = attack.BaseDamage;
                int attackFromAttacker = Mathf.RoundToInt(player.Stats.CurrentAttack * 0.5f);

                int damage = player.Target.Stats.TakeDamage(baseDamage, attackFromAttacker);
                attack.SpawnHitVFX(player.Target.transform);
                attack.TotalDamage += damage;
                attack.TotalHits++;
                attack.UpdateTotalHits();
                Debug.Log(damage);

                // display damage
                damageText.text = "Damage: " + damage;

                return true;
            }

            return false;
        }
    }
}