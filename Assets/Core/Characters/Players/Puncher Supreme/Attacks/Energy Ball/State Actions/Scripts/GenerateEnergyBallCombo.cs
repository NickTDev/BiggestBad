// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Generates a combo for the Energy Ball attack.
    /// </summary>
    [CreateAssetMenu(fileName = "Generate Energy Ball Combo", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Generate Energy Ball Combo")]
    public class GenerateEnergyBallCombo : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnergyBall attack = (EnergyBall)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            attack.GenerateCombo();
            player.ChangeAnimation("energyCharge");
        }
    }
}