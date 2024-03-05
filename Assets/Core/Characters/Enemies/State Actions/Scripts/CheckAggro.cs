// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Checks if a player is in aggro range.
    /// </summary>
    [CreateAssetMenu(fileName = "Check Aggro", menuName = "Behavior Tree Pattern/New Action/Common/Enemy/Check Aggro")]
    public class CheckAggro : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnemyBattleController enemy = (EnemyBattleController)states;

            // get array of colliders
            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, enemy.AggroRange, enemy.CharacterLayer, QueryTriggerInteraction.Collide);
            
            // perform logic if hitcolliders has objects
            if (hitColliders.Length > 0)
            {
                foreach (Collider col in hitColliders)
                {
                    if (col.CompareTag("Player")) // if anything in the collider is a player, is aggroed is true
                        enemy.IsAggroed = true;
                }
            }
        }
    }
}