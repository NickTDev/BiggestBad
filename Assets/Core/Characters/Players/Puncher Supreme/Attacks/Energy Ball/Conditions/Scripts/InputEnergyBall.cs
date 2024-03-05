// Merle Roji

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// The input for the energy ball skill.
    /// </summary>
    [CreateAssetMenu(fileName = "Input Energy Ball", menuName = "Behavior Tree Pattern/New Condition/Specific/Puncher Supreme/Input Energy Ball")]
    public class InputEnergyBall : Condition
    {
        private void OnValidate()
        {
            Description = "Inputted correct input?";
        }

        public override bool CheckCondition(StateManager state)
        {
            EnergyBall attack = (EnergyBall)state;
            PlayerBattleController player = attack.GetCharacter() as PlayerBattleController;

            // create a temporary integer that does not match any button press.
            int currentPress = -1;
            bool pressedAllInputs = false;

            // depending on the attack, the current press changes.
            if (player.Player1ActionPressed)
                currentPress = 0;
            else if (player.Player2ActionPressed)
                currentPress = 1;
            else if (player.Player3ActionPressed)
                currentPress = 2;
            else if (player.Player4ActionPressed)
                currentPress = 3;

            // check if the inputs were almost made
            for (int i = 0; i < attack.RecordedMoveInputs.Count; i++)
            {
                bool ifAlmostFirstInput =
                    (attack.RecordedMoveInputs[i].x + 0.1f <= attack.RequiredMoveString[0].x ||
                    attack.RecordedMoveInputs[i].x - 0.1f >= attack.RequiredMoveString[0].x) &&
                    (attack.RecordedMoveInputs[i].y + 0.1f <= attack.RequiredMoveString[0].y ||
                    attack.RecordedMoveInputs[i].y - 0.1f >= attack.RequiredMoveString[0].y);

                //bool ifAlmostSecondInput =
                //    (attack.RecordedMoveInputs[i].x + 0.1f <= attack.RequiredMoveString[1].x ||
                //    attack.RecordedMoveInputs[i].x - 0.1f >= attack.RequiredMoveString[1].x) &&
                //    (attack.RecordedMoveInputs[i].y + 0.1f <= attack.RequiredMoveString[1].y ||
                //    attack.RecordedMoveInputs[i].y - 0.1f >= attack.RequiredMoveString[1].y);

                bool ifAlmostThirdInput =
                    (attack.RecordedMoveInputs[i].x + 0.1f <= attack.RequiredMoveString[1].x ||
                    attack.RecordedMoveInputs[i].x - 0.1f >= attack.RequiredMoveString[1].x) &&
                    (attack.RecordedMoveInputs[i].y + 0.1f <= attack.RequiredMoveString[1].y ||
                    attack.RecordedMoveInputs[i].y - 0.1f >= attack.RequiredMoveString[1].y);

                if (ifAlmostFirstInput && /*ifAlmostSecondInput &&*/ ifAlmostThirdInput)
                    pressedAllInputs = true;
            }

            // check current press against the combo string.
            if (currentPress == attack.RequiredInput)
            {
                //Debug.Log(pressedAllInputs);

                if (pressedAllInputs)
                {
                    // change animation
                    player.ChangeAnimation("energyBall");
                
                    return true;
                }
            }

            return false;
        }
    }
}