// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the shield is destroyed.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Shield Destroyed", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Counter/Is Shield Destroyed")]
    public class IsShieldDestroyed : Condition
    {
        private void OnValidate()
        {
            Description = "Is the Shield gone?";
        }

        public override bool CheckCondition(StateManager state)
        {
            ShieldCounter counter = (ShieldCounter)state;
            PlayerBattleController player = counter.GetCharacter() as PlayerBattleController;

            if (!counter.Shield)
            {
                player.Stats.SetDefense(counter.PreviousCurrentDefense);
                return true;
            }

            return false;
        }
    }
}
