// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Change animation to the bomb's danger.
    /// </summary>
    [CreateAssetMenu(fileName = "Change Animation to Danger", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Change Animation to Danger")]
    public class ChangeAnimationToDanger : StateActions
    {
        public override void Execute(StateManager states)
        {
            Cact4Attack attack = (Cact4Attack)states;
            Animator bombAnimator = attack.Bomb.GetComponent<Animator>();

            AnimationQoL.ChangeAnimation(bombAnimator, "closeBy");
        }
    }
}
