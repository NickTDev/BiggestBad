// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Destroys the Corrosive Bomb object.
    /// </summary>
    [CreateAssetMenu(fileName = "Destroy Corrosive Bomb", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Destroy Corrosive Bomb")]
    public class DestroyCorrosiveBomb : StateActions
    {
        public override void Execute(StateManager states)
        {
            CorrosiveBomb attack = (CorrosiveBomb)states;
            attack.DestroyBomb();
        }
    }
}