// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Destroys the needle barrage object.
    /// </summary>
    [CreateAssetMenu(fileName = "Destroy Needles", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Destroy Needles")]
    public class DestroyNeedles : StateActions
    {
        public override void Execute(StateManager states)
        {
            Needles attack = (Needles)states;
            attack.DestroyNeedles();
        }
    }
}