// Nicholas Tvaroha

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Applies necessary stat changes based on currently held status effects
    /// </summary>
    [CreateAssetMenu(fileName = "Apply Status Effects", menuName = "Behavior Tree Pattern/New Action/Common/Character/Apply Status Effects")]
    public class ApplyStatusEffects : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;

            for (int i = 0; i < character.StatusEffects.Count; i++)
            {
                character.StatusEffects[i].ApplyEffect(character);
            }
        }
    }
}
