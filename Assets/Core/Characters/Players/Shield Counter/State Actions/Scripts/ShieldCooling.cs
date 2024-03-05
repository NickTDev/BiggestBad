// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Cools the Shield gauge.
    /// </summary>
    [CreateAssetMenu(fileName = "Cooling Shield", menuName = "Behavior Tree Pattern/New Action/Common/Player/Counter/Cooling Shield")]
    public class CoolingShield : StateActions
    {
        public override void Execute(StateManager states)
        {
            ShieldCounter counter = (ShieldCounter)states;

            if (counter.ChargeSlider.value > 0f)
                counter.ChargeSlider.value -= Time.fixedDeltaTime;
            else
                counter.ChargeSlider.value = 0f;

            float counterValue = counter.ChargeSlider.value;
            float maxValue = counter.ChargeSlider.maxValue;
            counter.PercentText = $"{Mathf.FloorToInt((counterValue / maxValue) * 100)}%";
        }
    }
}