// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Injects the beginning position of the user and the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Inject Positions", menuName = "Behavior Tree Pattern/New Action/Common/Character/Inject Positions")]
    public class InjectUserAndTargetPositions : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            attack.InitialPosition = character.transform.position;
            attack.InitialTargetPosition = character.Target.transform.position;

            attack.DestroyFollowCam();
        }
    }
}