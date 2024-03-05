// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Fires a Corrosive Bomb at a targeted opponent.
    /// </summary>
    [CreateAssetMenu(fileName = "Fire Corrosive Bomb", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Fire Corrosive Bomb")]
    public class FireCorrosiveBomb : StateActions
    {
        public override void Execute(StateManager states)
        {
            CorrosiveBomb attack = (CorrosiveBomb)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            attack.SpawnBomb();
            player.SoundManager.PlayOneShot(attack._throwSound);
        }
    }
}