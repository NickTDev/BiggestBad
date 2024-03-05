// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Destroys the grenade object.
    /// </summary>
    [CreateAssetMenu(fileName = "Destroy Grenade", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Destroy Grenade")]
    public class DestroyGrenade : StateActions
    {
        public override void Execute(StateManager states)
        {
            GrenadeToss attack = (GrenadeToss)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            attack.DestroyGrenade();
            player.SoundManager.PlayOneShot(attack._explodeSound);
        }
    }
}