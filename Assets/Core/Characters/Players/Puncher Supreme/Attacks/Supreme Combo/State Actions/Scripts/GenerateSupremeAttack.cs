// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Generates an attack for the Supreme Combo.
    /// </summary>
    [CreateAssetMenu(fileName = "Generate Supreme Attack", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Generate Supreme Attack")]
    public class GenerateSupremeAttack : StateActions
    {
        public override void Execute(StateManager states)
        {
            SupremeCombo attack = (SupremeCombo)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            attack.GenerateInput();
            player.SoundManager.PlayOneShot(attack._comboSound);
        }
    }
}