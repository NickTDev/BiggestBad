// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Resets the time back to max.
    /// </summary>
    [CreateAssetMenu(fileName = "Reset Time", menuName = "Behavior Tree Pattern/New Action/Common/Character/Reset Time")]
    public class ResetTime : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            attack.CurrentTime = attack.MaxTime;
        }
    }
}
