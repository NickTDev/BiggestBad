// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Finishes the attack.
    /// </summary>
    [CreateAssetMenu(fileName = "Finish Attack", menuName = "Behavior Tree Pattern/New Action/Common/Character/Finish Attack")]
    public class FinishAttack : StateActions
    {
        public override void Execute(StateManager states)
        {
            MinigameStateMachine attack = (MinigameStateMachine)states;
            CharacterBattleController character = attack.GetCharacter();

            attack.CurrentTime += Time.deltaTime;

            if (attack.CurrentTime >= attack.FinishTime)
            {
                character.FinishTurn();

                Destroy(attack.gameObject);
            }
        }
    }
}
