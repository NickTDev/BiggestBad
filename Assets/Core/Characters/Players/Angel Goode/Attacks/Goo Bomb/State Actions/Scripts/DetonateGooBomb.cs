// Merle Roji

using UnityEngine;
using TMPro;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Detonates the Goo Bomb.
    /// </summary>
    [CreateAssetMenu(fileName = "Detonate Goo Bomb", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Detonate Goo Bomb")]
    public class DetonateGooBomb : StateActions
    {
        public override void Execute(StateManager states)
        {
            GooBomb attack = (GooBomb)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;
            attack.SpawnExplosion();
            player.SoundManager.PlayOneShot(attack._gooSound);
        }
    }
}