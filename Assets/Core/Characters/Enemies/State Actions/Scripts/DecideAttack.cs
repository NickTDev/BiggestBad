// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Chooses which attack to use.
    /// </summary>
    [CreateAssetMenu(fileName = "Decide Attack", menuName = "Behavior Tree Pattern/New Action/Common/Enemy/Decide Attack", order = 0)]
    public class DecideAttack : StateActions
    {
        public override void Execute(StateManager states)
        {
            EnemyBattleController enemy = (EnemyBattleController)states;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject targetPlayer = null;

            // if not aggroed, just do nothing
            if (!enemy.IsAggroed) return;

            /// choose attack based on damage, energy and a little bit of weighted random.
            var chosenAttack = 0;

            // getting a random weight value based on damage and energy
            int totalWeight = 0;
            foreach(MinigameStateMachine skill in enemy.SkillList)
            {
                // energy is weighted as half since values usually higher than damage
                totalWeight += skill.BaseDamage + Mathf.RoundToInt(skill.BaseEnergyValue * 0.5f);
            }
            int randWeightValue = Random.Range(1, totalWeight + 1);

            // Checking where random weight value might fall
            int processedWeight = 0;
            for (int a = 0; a < enemy.SkillList.Length; a++)
            {
                // energy is weighted as half since values usually higher than damage
                processedWeight += enemy.SkillList[a].BaseDamage + Mathf.RoundToInt(enemy.SkillList[a].BaseEnergyValue * 0.5f);
                if (randWeightValue <= processedWeight) // if the processed weight is higher than the random weight value
                {
                    // checking if you have the required amount of energy needed to use a spend skill
                    if (enemy.SkillList[a].Type == EnergyType.Spend)
                    {
                        if (enemy.Stats.CurrentEnergy > 0 && enemy.Stats.CurrentEnergy >= enemy.SkillList[a].BaseEnergyValue)
                            chosenAttack = a;
                    }
                    else if (enemy.SkillList[a].Type == EnergyType.Gain)
                    {
                        // if its a gain skill, just choose it
                        chosenAttack = a;
                    }
                }
            }

            // choose attack
            enemy.ChosenAttackIndex = chosenAttack;

            // cycle through players in range based on who's closer.
            if (players.Length > 0)
            {
                for (int p = 0; p < players.Length; p++)
                {
                    if (targetPlayer == null)
                        targetPlayer = players[p];
                    else if ((Vector3.Distance(enemy.transform.position, targetPlayer.transform.position) >
                             Vector3.Distance(enemy.transform.position, players[p].transform.position) &&
                             players[p].GetComponent<PlayerBattleController>().Stats.CurrentHP > 0))
                        targetPlayer = players[p];

                }

                if (targetPlayer.TryGetComponent<CharacterBattleController>(out CharacterBattleController potentialTarget))
                {
                    enemy.Target = potentialTarget;
                }
            }

            if (enemy.IsAggroed) // if the enemy is aggroed, perform decisions
            {
                if (Vector3.Distance(enemy.transform.position, targetPlayer.transform.position) <= enemy.SkillList[enemy.ChosenAttackIndex].Range)
                    enemy.IsAttacking = true;
                else if (enemy.Stats.BaseSpeed > 0)
                    enemy.IsMoving = true;
            }
        }
    }
}