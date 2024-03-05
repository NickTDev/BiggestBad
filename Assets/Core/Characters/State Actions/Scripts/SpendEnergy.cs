// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Spends energy from the character based on performance of the minigame.
    /// </summary>
    [CreateAssetMenu(fileName = "Spend Energy", menuName = "Behavior Tree Pattern/New Action/Common/Character/Spend Energy")]
    public class SpendEnergy : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            var finalEnergy = attack.BaseEnergyValue;
            character.Stats.SpendEnergy(finalEnergy);
        }
    }
}