// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Zeroes out the timer.
    /// </summary>
    [CreateAssetMenu(fileName = "Zero Time", menuName = "Behavior Tree Pattern/New Action/Common/Character/Zero Time")]
    public class ZeroTime : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            attack.CurrentTime = 0f;
        }
    }
}
