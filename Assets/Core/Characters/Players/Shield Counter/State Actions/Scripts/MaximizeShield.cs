// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Maximizes the Shield Counter.
    /// </summary>
    [CreateAssetMenu(fileName = "Maximize Shield", menuName = "Behavior Tree Pattern/New Action/Common/Player/Counter/Maximize Shield")]
    public class MaximizeShield : StateActions
    {
        public override void Execute(StateManager states)
        {
            ShieldCounter counter = (ShieldCounter)states;
            CharacterBattleController player = counter.GetCharacter();

            counter.ChargeSlider.value = counter.ChargeSlider.maxValue;
            float counterValue = counter.ChargeSlider.value;
            float maxValue = counter.ChargeSlider.maxValue;
            counter.PercentText = $"{Mathf.FloorToInt((counterValue / maxValue) * 100)}%";
            counter.SetFillCooldownColor();
            player.ChangeAnimation("stance");
        }
    }
}