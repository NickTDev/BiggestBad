// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the character using the attack exists.
    /// </summary>
    [CreateAssetMenu(fileName = "Does User Exist", menuName = "Behavior Tree Pattern/New Condition/Attacks/Does User Exist")]
    public class DoesUserExist : Condition
    {
        private void OnValidate()
        {
            Description = "Does the user of this Minigame exist?";
        }

        public override bool CheckCondition(StateManager state)
        {
            MinigameStateMachine attack = state as MinigameStateMachine;
            CharacterBattleController character = attack.GetCharacter();
            attack.SetReadyText();

            // little pause before attack starts
            attack.CurrentTime += Time.deltaTime;
            if (attack.CurrentTime >= attack.FinishTime)
            {
                if (character != null)
                {
                    attack.TurnOffReadyText();
                    return true;
                }
            }

            return false;
        }
    }
}
