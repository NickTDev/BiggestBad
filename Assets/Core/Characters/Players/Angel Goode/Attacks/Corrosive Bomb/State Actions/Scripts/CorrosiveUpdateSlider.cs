// Merle Roji

using System.Collections;
using System.Collections.Generic;
using T02.Characters.InBattle;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Slides the slider up and down based on a wave, corrosive bomb version.
    /// </summary>
    [CreateAssetMenu(fileName = "Update Corrosive Slider", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Update Corrosive Slider")]
    public class CorrosiveUpdateSlider : StateActions
    {
        public override void Execute(StateManager states)
        {
            CorrosiveBomb attack = (CorrosiveBomb)states;

            if (attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(false); }

            // set wave parameters
            float amplitude = attack.CurrentSlider.maxValue * 0.5f;
            float phase = attack.CurrentSlider.maxValue * 0.5f;

            // count down time and play wave
            if (attack.CurrentTime > 0f)
                attack.CurrentTime -= Time.deltaTime;
            else
                attack.CurrentTime = 0f;
            float cosineWave = (amplitude * -Mathf.Cos(attack.CurrentTime)) + phase;
            attack.CurrentSlider.value = cosineWave;

            // display time left
            string time = attack.CurrentTime.ToString("F1") + " sec";
            attack.DisplayTimerText(time);
        }
    }
}