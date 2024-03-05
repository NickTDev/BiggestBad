// Merle Roji

using System.Collections;
using System.Collections.Generic;
using T02.Characters.InBattle;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Slides the slider up and down based on a wave, grenade version.
    /// </summary>
    [CreateAssetMenu(fileName = "Grenade Slider", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Grenade Slider")]
    public class SlideHandleSinewave : StateActions
    {
        public override void Execute(StateManager states)
        {
            GrenadeToss attack = (GrenadeToss)states;

            if (attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(false); }

            // set wave parameters
            float amplitude = attack.AttackSlider.maxValue * 0.5f;
            float phase = attack.AttackSlider.maxValue * 0.5f;

            // count down time and play wave
            if (attack.CurrentTime > 0f)
                attack.CurrentTime -= Time.deltaTime;
            else
                attack.CurrentTime = 0f;
            float cosineWave = (amplitude * -Mathf.Cos(attack.CurrentTime)) + phase;
            attack.AttackSlider.value = cosineWave;
            
            // display time left
            string time = attack.CurrentTime.ToString("F1") + " sec";
            attack.DisplayTimerText(time);

            // get the percentage of value over max value
            float percentile = attack.AttackSlider.value / attack.AttackSlider.maxValue;

            // calculate damage based on performance
            if (percentile <= 0.2f || percentile >= 0.8f)
            {
                attack.Damage = attack.BaseDamage;
                attack.AdjustedEnergyValue = -5;
            }
            else if (percentile <= 0.325f || percentile >= 0.675f)
            {
                attack.Damage = attack.BaseDamage * 2;
                attack.AdjustedEnergyValue = 0;
            }
            else if (percentile <= 0.475f || percentile >= 0.525f)
            {
                attack.Damage = attack.BaseDamage * 3;
                attack.AdjustedEnergyValue = 5;
            }
            else
            {
                attack.Damage = attack.BaseDamage * 4;
                attack.AdjustedEnergyValue = 10;
            }
        }
    }
}
