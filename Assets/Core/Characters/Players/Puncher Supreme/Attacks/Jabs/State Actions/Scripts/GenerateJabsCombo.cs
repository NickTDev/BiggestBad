// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Generates a combo for the Jabs attack.
    /// </summary>
    [CreateAssetMenu(fileName = "Generate Jabs Combo", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Generate Jabs Combo")]
    public class GenerateJabsCombo : StateActions
    {
        public override void Execute(StateManager states)
        {
            Jabs attack = (Jabs)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            attack.GenerateCombo();
            player.SoundManager.PlayOneShot(attack._jabSound);
        }
    }
}