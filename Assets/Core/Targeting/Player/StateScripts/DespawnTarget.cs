// Nicholas Tvaroha

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Destroys the player's target from the world
    /// </summary>
    [CreateAssetMenu(fileName = "Despawn Target", menuName = "Behavior Tree Pattern/New Action/Common/Player/Despawn Target")]
    public class DespawnTarget : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController player = (PlayerBattleController)states;
            player.DestroyTileSelector();
        }
    }
}
