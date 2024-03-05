// Merle Roji

using UnityEngine;
using TMPro;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Create a Midway transform object to allow the camera to follow.
    /// </summary>
    [CreateAssetMenu(fileName = "Create Midway Follow", menuName = "Behavior Tree Pattern/New Action/Common/Character/Create Midway Follow")]
    public class CreateMidwayFollow : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            if (!attack.FollowCam)
            {
                attack.SpawnFollowCam(character.transform, character.Target.transform);
            }
        }
    }
}
