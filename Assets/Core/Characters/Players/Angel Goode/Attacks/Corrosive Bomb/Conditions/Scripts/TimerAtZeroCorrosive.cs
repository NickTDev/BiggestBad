// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the timer has started ticking, corrosive edition.
    /// </summary>
    [CreateAssetMenu(fileName = "Timer at Zero Corrosive", menuName = "Behavior Tree Pattern/New Condition/Specific/Angel Goode/Timer at Zero Corrosive")]
    public class TimerAtZeroCorrosive : Condition
    {
        private void OnValidate()
        {
            Description = "Is the timer at zero?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CorrosiveBomb attack = (CorrosiveBomb)state;

            return attack.CurrentTime <= 0 && attack.SliderIndex < attack.AmountOfSliders - 1;
        }
    }
}
