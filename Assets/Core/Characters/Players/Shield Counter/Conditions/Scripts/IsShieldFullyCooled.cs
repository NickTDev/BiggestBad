// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the shield gauge is fully cooled.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Shield Cooled", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Counter/Is Shield Cooled")]
    public class IsShieldFullyCooled : Condition
    {
        private void OnValidate()
        {
            Description = "Is the Shield Gauge fully cooled?";
        }

        public override bool CheckCondition(StateManager state)
        {
            ShieldCounter counter = (ShieldCounter)state;
            CharacterBattleController player = counter.GetCharacter();

            bool isShieldFullyCooled = counter.ChargeSlider.value == 0;

            if (isShieldFullyCooled)
            {
                float counterValue = counter.ChargeSlider.value;
                float maxValue = counter.ChargeSlider.maxValue;
                counter.PercentText = $"{Mathf.FloorToInt((counterValue / maxValue) * 100)}%";
                counter.SetFillReadyColor();
                player.ChangeAnimation("fightStance");

                return true;
            }

            return false;
        }
    }
}
