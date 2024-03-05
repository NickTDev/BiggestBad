// Merle Roji

using System.Collections;
using System.Collections.Generic;
using T02.Characters.InBattle;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Updates the Corrosive Slider index.
    /// </summary>
    [CreateAssetMenu(fileName = "Update Corrosive Index", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Update Corrosive Index")]
    public class UpdateCorrosiveSliderIndex : StateActions
    {
        public override void Execute(StateManager states)
        {
            CorrosiveBomb attack = (CorrosiveBomb)states;

            // get the percentage of value over max value
            float percentile = attack.CurrentSlider.value / attack.CurrentSlider.maxValue;
            if (percentile >= 0.325f && percentile <= 0.675f) // success
            {
                if(attack.SuccessCount < attack.AmountOfSliders)
                {
                    attack.SuccessCount++;
                    attack.UpdateSuccessCount();
                }
            }

            if (attack.SliderIndex < attack.AmountOfSliders - 1)
            {
                attack.SliderIndex++;
                attack.UpdateMaxTime();
            }
        }
    }
}