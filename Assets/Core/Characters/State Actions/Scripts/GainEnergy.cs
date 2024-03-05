// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Gives the character energy based on performance of the minigame.
    /// </summary>
    [CreateAssetMenu(fileName = "Gain Energy", menuName = "Behavior Tree Pattern/New Action/Common/Character/Gain Energy")]
    public class GainEnergy : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            var finalEnergy = attack.BaseEnergyValue + attack.AdjustedEnergyValue;
            character.Stats.GainEnergy(finalEnergy);
        }
    }
}