// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Charges the Shield Counter.
    /// </summary>
    [CreateAssetMenu(fileName = "Charging Shield Value", menuName = "Behavior Tree Pattern/New Action/Common/Player/Counter/Charging Shield Value")]
    public class ChargingShieldValue : StateActions
    {
        public override void Execute(StateManager states)
        {
            ShieldCounter counter = (ShieldCounter)states;
            PlayerBattleController player = counter.GetCharacter() as PlayerBattleController;

            if (player.Player1ActionHeld)
            {
                if (counter.ChargeSlider.value < counter.ChargeSlider.maxValue)
                    counter.ChargeSlider.value += Time.fixedDeltaTime;
                else
                    counter.ChargeSlider.value = counter.ChargeSlider.maxValue;
            }
            else
            {
                if (counter.ChargeSlider.value > 0f)
                    counter.ChargeSlider.value -= Time.fixedDeltaTime;
                else
                    counter.ChargeSlider.value = 0f;
            }

            float counterValue = counter.ChargeSlider.value;
            float maxValue = counter.ChargeSlider.maxValue;
            counter.PercentText = $"{Mathf.FloorToInt((counterValue / maxValue) * 100)}%";
        }
    }
}