// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Charges the needle attack.
    /// </summary>
    [CreateAssetMenu(fileName = "Charge Up Needles", menuName = "Behavior Tree Pattern/New Action/Specific/Cactus/Charge Up Needles")]
    public class ChargeUpNeedles : StateActions
    {
        public override void Execute(StateManager states)
        {
            Needles attack = (Needles)states;
            EnemyBattleController enemy = attack.GetCharacter() as EnemyBattleController;

            if (attack.CurrentTime < attack.MaxTime)
                attack.CurrentTime += Time.deltaTime;
            else
                attack.CurrentTime = attack.MaxTime;

            enemy.ChangeAnimation("needlesCharge");
            enemy.CharacterSprite.color = Color.Lerp(Color.white, attack.FullChargeColor, attack.CurrentTime);
            Debug.Log(enemy.CharacterSprite.color);
        }
    }
}