// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Ticks down the timer.
    /// </summary>
    [CreateAssetMenu(fileName = "Tick Down Timer", menuName = "Behavior Tree Pattern/New Action/Common/Character/Tick Down Timer")]
    public class TickDownTimer : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            attack.CurrentTime -= Time.deltaTime;
        }
    }
}
