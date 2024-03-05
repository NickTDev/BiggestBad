// Merle Roji

using T02.Characters.InBattle;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Counts the timer down during the Jab attack.
    /// </summary>
    [CreateAssetMenu(fileName = "Jab Timer Countdown", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Jab Timer Countdown")]
    public class JabTimerCountdown : StateActions
    {
        public override void Execute(StateManager states)
        {
            Jabs attack = (Jabs)states;

            // count down time and play wave
            if (attack.CurrentTime > 0f)
                attack.CurrentTime -= Time.deltaTime;
            else
                attack.CurrentTime = 0f;

            attack.AttackSlider.value = attack.CurrentTime;

            // display time left
            string time = attack.CurrentTime.ToString("F1") + " sec";
            attack.DisplayTimerText(time);
        }
    }
}
