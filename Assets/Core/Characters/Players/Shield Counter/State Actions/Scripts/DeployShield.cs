// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Deploys the shield.
    /// </summary>
    [CreateAssetMenu(fileName = "Deploy Shield", menuName = "Behavior Tree Pattern/New Action/Common/Player/Counter/Deploy Shield")]
    public class DeployShield : StateActions
    {
        public override void Execute(StateManager states)
        {
            ShieldCounter counter = (ShieldCounter)states;
            PlayerBattleController player = counter.GetCharacter() as PlayerBattleController;

            counter.SpawnShield();
            player.SoundManager.PlayOneShot(counter._shieldSound);
            player.ChangeAnimation("dodge");
        }
    }
}