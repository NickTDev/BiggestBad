// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the timer has started ticking.
    /// </summary>
    [CreateAssetMenu(fileName = "Timer has Started", menuName = "Behavior Tree Pattern/New Condition/Common/Timer has Started")]
    public class TimerHasStarted : Condition
    {
        private void OnValidate()
        {
            Description = "Has the timer started?";
        }

        public override bool CheckCondition(StateManager state)
        {
            MinigameStateMachine attack = (MinigameStateMachine)state;

            return attack.CurrentTime > 0;
        }
    }
}
