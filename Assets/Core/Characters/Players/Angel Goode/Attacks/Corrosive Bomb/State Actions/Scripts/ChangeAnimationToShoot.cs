// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Change animation to Angel's shoot.
    /// </summary>
    [CreateAssetMenu(fileName = "Change Animation to Shoot", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Change Animation to Shoot")]
    public class ChangeAnimationToShoot : StateActions
    {
        const string ANIM = "shoot";

        public override void Execute(StateManager states)
        {
            MinigameStateMachine skill = states as MinigameStateMachine;
            CharacterBattleController character = skill.GetCharacter();
            character.ChangeAnimation(ANIM);
        }
    }
}
