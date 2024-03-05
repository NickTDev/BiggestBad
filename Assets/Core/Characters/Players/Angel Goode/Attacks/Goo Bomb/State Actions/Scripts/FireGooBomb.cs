// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Fires a Goo Bomb at a targeted opponent.
    /// </summary>
    [CreateAssetMenu(fileName = "Fire Goo Bomb", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Fire Goo Bomb")]
    public class FireGooBomb : StateActions
    {
        public override void Execute(StateManager states)
        {
            GooBomb attack = (GooBomb)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            attack.SpawnBomb();
            player.SoundManager.PlayOneShot(attack._throwSound);
        }
    }
}