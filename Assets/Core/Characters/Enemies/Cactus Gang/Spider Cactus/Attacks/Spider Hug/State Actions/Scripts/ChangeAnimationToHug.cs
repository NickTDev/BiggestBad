// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Change animation to a character's hug.
    /// </summary>
    [CreateAssetMenu(fileName = "Change Animation to Hug", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Animation/Change Animation to Hug")]
    public class ChangeAnimationToHug : StateActions
    {
        public override void Execute(StateManager states)
        {
            BasicTackleAttack attack = (BasicTackleAttack)states;
            CharacterBattleController character = attack.GetCharacter();
            character.ChangeAnimation("hug");
        }
    }
}
