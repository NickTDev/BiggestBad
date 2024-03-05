// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks to see if the turn manager is injected into the character.
    /// </summary>
    [CreateAssetMenu(fileName = "Is Turn Manager Injected", menuName = "Behavior Tree Pattern/New Condition/Common/Character/Is Turn Manager Injected")]
    public class IsTurnManagerInjected : Condition
    {
        private void OnValidate()
        {
            Description = "Is the turn manager injected?";
        }

        public override bool CheckCondition(StateManager state)
        {
            CharacterBattleController character = (CharacterBattleController)state;

            return character.DoesTurnManagerAndTurnInfoExist();
        }
    }
}
