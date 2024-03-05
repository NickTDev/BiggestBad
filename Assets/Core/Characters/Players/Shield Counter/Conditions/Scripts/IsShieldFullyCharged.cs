// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the shield gauge is fully charged and the button is released.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Shield Charged", menuName = "Behavior Tree Pattern/New Condition/Common/Player/Counter/Is Shield Charged")]
    public class IsShieldFullyCharged : Condition
    {
        private void OnValidate()
        {
            Description = "Is the Shield Gauge fully charged\n" +
                           "and the button is released?";
        }

        public override bool CheckCondition(StateManager state)
        {
            ShieldCounter counter = (ShieldCounter)state;
            PlayerBattleController player = counter.GetCharacter() as PlayerBattleController;

            bool isShieldFullyCharged = Mathf.RoundToInt(counter.ChargeSlider.value) == Mathf.RoundToInt(counter.ChargeSlider.maxValue);

            if (isShieldFullyCharged)
            {
                if (player.Player1ActionReleased)
                {
                    counter.ChargeSlider.value = counter.ChargeSlider.maxValue;
                    float counterValue = counter.ChargeSlider.value;
                    float maxValue = counter.ChargeSlider.maxValue;
                    counter.PercentText = $"{Mathf.FloorToInt((counterValue / maxValue) * 100)}%";
                    counter.SetFillCooldownColor();

                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
