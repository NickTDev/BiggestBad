// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Destroys the energy ball object.
    /// </summary>
    [CreateAssetMenu(fileName = "Destroy Energy Ball", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Destroy Energy Ball")]
    public class DestroyEnergyBall : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnergyBall attack = (EnergyBall)states;
            attack.DestroyProjectile();
        }
    }
}