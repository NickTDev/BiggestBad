// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Spends energy from the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Target Spend Energy", menuName = "Behavior Tree Pattern/New Action/Common/Character/Target Spend Energy")]
    public class TargetSpendEnergy : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController target = attack.GetCharacter().Target;

            target.Stats.SpendEnergy(attack.CounteredEnergyValue);
        }
    }
}