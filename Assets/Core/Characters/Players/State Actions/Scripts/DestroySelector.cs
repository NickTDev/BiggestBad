// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Destroys the selector.
    /// </summary>
    [CreateAssetMenu(fileName = "Destroy Selector", menuName = "Behavior Tree Pattern/New Action/Common/Character/Destroy Selector")]
    public class DestroySelector : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;

            //Destroy(character.Selector);
            //character.Selector = null;
        }
    }
}