// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Change animation to Angel's Grenade Toss.
    /// </summary>
    [CreateAssetMenu(fileName = "Change Animation to Tossing Grenade", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Change Animation to Tossing Grenade")]
    public class ChangeAnimationToTossingGrenade : StateActions
    {
        const string ANIM = "tossingGrenade";

        public override void Execute(StateManager states)
        {
            MinigameStateMachine skill = states as MinigameStateMachine;
            CharacterBattleController character = skill.GetCharacter();
            character.ChangeAnimation(ANIM);
        }
    }
}
