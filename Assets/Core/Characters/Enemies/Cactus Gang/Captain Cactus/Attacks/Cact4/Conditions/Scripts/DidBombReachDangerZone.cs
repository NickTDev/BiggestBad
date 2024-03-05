// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the cact4 bomb reached the danger zone.
    /// </summary>
    [CreateAssetMenu(fileName = "Did Cact4 Reach DangerZone", menuName = "Behavior Tree Pattern/New Condition/Specific/Cactus/Did Cact4 Reach DangerZone")]
    public class DidCact4BombReachDangerZone : Condition
    {
        private void OnValidate()
        {
            Description = "Did the bomb reach the danger zone?";
        }

        public override bool CheckCondition(StateManager state)
        {
            Cact4Attack attack = (Cact4Attack)state;
            return attack.DidBombReachDangerZone;
        }
    }
}