// Merle Roji

using T02.Characters.InBattle;
using UnityEngine;

namespace T02
{
    /// <summary>
    /// Slides the slider up and down based on a button press and color panel color.
    /// </summary>
    [CreateAssetMenu(fileName = "Update Goo Slider", menuName = "Behavior Tree Pattern/New Action/Specific/Angel Goode/Update Goo Slider")]
    public class UpdateGooSlider : StateActions
    {
        public override void Execute(StateManager states)
        {
            GooBomb attack = (GooBomb)states;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            if (attack.DamagePanel.activeInHierarchy) { attack.DamagePanel.SetActive(false); }

            // count down time
            if (attack.CurrentTime > 0f)
                attack.CurrentTime -= Time.deltaTime;
            else
                attack.CurrentTime = 0f;

            // count down color swap time
            if (attack.ColorswapTimer > 0f)
                attack.ColorswapTimer -= Time.deltaTime;
            else
            {
                attack.TogglePanelColor();
                attack.ResetColorswapTimer();
            }

            if (attack.ButtonHeld)
            {
                if (attack.ColorPanel.color == Color.green)
                {
                    attack.BombSlider.value += attack.SliderSpeed * Time.fixedDeltaTime;
                }
                else if (attack.ColorPanel.color == Color.red)
                {
                    attack.BombSlider.value -= attack.SliderSpeed * Time.fixedDeltaTime;
                }
            }

            // get the percentage of value over max value
            float percentile = attack.BombSlider.value / attack.BombSlider.maxValue;

            // damage calc
            if (percentile < 0.50f)
            {
                attack.Damage = attack.BaseDamage;
                attack.AmountOfTurns = 0;
            }
            else if (percentile < 0.875f)
            {
                attack.Damage = attack.BaseDamage + Mathf.RoundToInt(0.5f * attack.BaseDamage);
                attack.AmountOfTurns = 2;
            }
            else
            {
                attack.Damage = attack.BaseDamage * 2;
                attack.AmountOfTurns = 3;
            }

            // display time left
            string time = attack.CurrentTime.ToString("F1") + " sec";
            attack.DisplayTimerText(time);
        }
    }
}