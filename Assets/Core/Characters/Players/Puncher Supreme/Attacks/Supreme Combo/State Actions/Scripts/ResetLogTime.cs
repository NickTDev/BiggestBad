// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Calculates current log time.
    /// </summary>
    [CreateAssetMenu(fileName = "Reset Log Time", menuName = "Behavior Tree Pattern/New Action/Specific/Puncher Supreme/Reset Log Time")]
    public class ResetLogTime : StateActions
    {
        public override void Execute(StateManager states)
        {
            SupremeCombo attack = (SupremeCombo)states;
            attack.MaxLogTime = attack.LogarithmicTime();
            attack.CurrentTime = attack.MaxLogTime;
            attack.AttackSlider.maxValue = attack.MaxLogTime;
        }
    }
}
