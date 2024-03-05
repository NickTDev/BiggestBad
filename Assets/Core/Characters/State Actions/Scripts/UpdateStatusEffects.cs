// Nicholas Tvaroha

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Updates status effects by decreasing their turn times and removing ones that have run out
    /// </summary>
    [CreateAssetMenu(fileName = "Update Status Effects", menuName = "Behavior Tree Pattern/New Action/Common/Character/Update Status Effects")]
    public class UpdateStatusEffects : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;

            if (character.StatusEffects.Count > 0)
            {
                for (int i = 0; i < character.StatusEffects.Count; i++)
                {
                    character.StatusEffects[i].NumTurns--;
                    if (character.StatusEffects[i].NumTurns <= 0)
                    {
                        character.StatusEffects[i].RemoveEffect(character);
                        character.StatusEffects.Remove(character.StatusEffects[i]);
                    }
                }
            }
        }
    }
}
