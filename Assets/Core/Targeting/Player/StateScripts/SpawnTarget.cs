// Nicholas Tvaroha

using TMPro;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Instantiates the player's target into the world
    /// </summary>
    [CreateAssetMenu(fileName = "Spawn Target", menuName = "Behavior Tree Pattern/New Action/Placeholder/Spawn Target")]
    public class SpawnTarget : StateActions
    {
        public override void Execute(StateManager states)
        {
            PlayerBattleController player = (PlayerBattleController)states;
            player.SpawnTileSelector();
            //Instantiate(Resources.Load("PlayerTarget"), player.transform.position + Vector3.right + (Vector3.down * 0.4f), Quaternion.identity);
        }
    }
}
