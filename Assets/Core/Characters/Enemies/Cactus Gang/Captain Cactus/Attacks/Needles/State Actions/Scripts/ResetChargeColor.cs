// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Resets the charging color.
    /// </summary>
    [CreateAssetMenu(fileName = "Reset Charge Color", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Reset Charge Color")]
    public class ResetChargeColor : StateActions
    {
        public override void Execute(StateManager states)
        {
            Needles attack = (Needles)states;
            EnemyBattleController enemy = attack.GetCharacter() as EnemyBattleController;
            enemy.CharacterSprite.color = Color.white;
        }
    }
}