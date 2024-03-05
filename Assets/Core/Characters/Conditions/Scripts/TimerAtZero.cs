// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the timer has started ticking.
    /// </summary>
    [CreateAssetMenu(fileName = "Timer at Zero", menuName = "Behavior Tree Pattern/New Condition/Common/Timer at Zero")]
    public class TimerAtZero : Condition
    {
        private void OnValidate()
        {
            Description = "Is the timer at zero?";
        }

        public override bool CheckCondition(StateManager state)
        {
            MinigameStateMachine attack = (MinigameStateMachine)state;

            return attack.CurrentTime <= 0;
        }
    }
}
