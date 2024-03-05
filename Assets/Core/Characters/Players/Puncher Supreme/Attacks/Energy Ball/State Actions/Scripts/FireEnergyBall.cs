// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Fires an Energy Ball at a targeted opponent.
    /// </summary>
    [CreateAssetMenu(fileName = "Fire Energy Ball", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Fire Energy Ball")]
    public class FireEnergyBall : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnergyBall attack = (EnergyBall)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            player.ChangeAnimation("energyBall");
            attack.SpawnEnergyBall();
            player.SoundManager.PlayOneShot(attack._energyBallSound);
        }
    }
}