// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Gives the target energy.
    /// </summary>
    [CreateAssetMenu(fileName = "Target Gain Energy", menuName = "Behavior Tree Pattern/New Action/Common/Character/Target Gain Energy")]
    public class TargetGainEnergy : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController target = attack.GetCharacter().Target;

            target.Stats.GainEnergy(attack.CounteredEnergyValue);
        }
    }
}