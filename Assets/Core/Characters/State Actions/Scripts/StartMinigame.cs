// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Starts the minigame.
    /// </summary>
    [CreateAssetMenu(fileName = "Start Minigame", menuName = "Behavior Tree Pattern/New Action/Common/Character/Start Minigame")]
    public class StartMinigame : StateActions
    {
        public override void Execute(StateManager states)
        {
            CharacterBattleController character = (CharacterBattleController)states;
            character.StartMinigame();
        }
    }
}
