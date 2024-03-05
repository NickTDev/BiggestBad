// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Fires a Grenade at a targeted opponent.
    /// </summary>
    [CreateAssetMenu(fileName = "Fire Grenade", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Fire Grenade")]
    public class FireGrenade : StateActions
    {
        public override void Execute(StateManager states)
        {
            GrenadeToss attack = (GrenadeToss)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            attack.SpawnGrenade();
            player.SoundManager.PlayOneShot(attack._throwSound);
        }
    }
}