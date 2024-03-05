// Merle Roji

using T02.Characters.InBattle;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Counts the timer down during the Energy Ball.
    /// </summary>
    [CreateAssetMenu(fileName = "Energy Ball Timer Countdown", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Energy Ball Timer Countdown")]
    public class EnergyBallTimerCountdown : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnergyBall attack = (EnergyBall)states;

            // count down time and play wave
            if (attack.CurrentTime > 0f)
                attack.CurrentTime -= Time.deltaTime;
            else
                attack.CurrentTime = 0f;

            attack.AttackSlider.value = attack.CurrentTime;
            //attack.Damage = Mathf.RoundToInt(attack.AttackSlider.value);

            // display time left
            string time = attack.CurrentTime.ToString("F1") + " sec";
            attack.DisplayTimerText(time);
        }
    }
}
