// Merle Roji

using UnityEngine;

namespace T02.Characters
{
    /// <summary>
    /// Stat containers 
    /// </summary>
    //[CreateAssetMenu(fileName = "Character Stat Container", menuName = "RPG System/Character/Create Stat Container", order = 0)]
    public class CharacterStatContainer : ScriptableObject
    {
        #region VARIABLES

        private CharacterStats _baseStats;

        [Header("Current Stats")]
        [SerializeField, Min(0)] private int _currentHP = 1;
        [SerializeField, Min(0)] private int _currentEnergy = 1;
        [SerializeField, Min(1)] private int _currentAttack = 1;
        [SerializeField, Min(1)] private int _currentDefense = 1;
        [SerializeField, Min(1)] private int _currentSpeed = 1;
        [SerializeField, Min(0)] private int _currentMovement = 1;

        #endregion

        #region STAT CALCULATIONS

        /// <summary>
        /// Initializes certain stats.
        /// </summary>
        public void InjectStats(CharacterStats baseStats)
        {
            _baseStats = baseStats;
            _currentHP = _baseStats.MaxHP;
            _currentEnergy = 0;
            _currentAttack = _baseStats.BaseAttack;
            _currentDefense = _baseStats.BaseDefense;
            _currentSpeed = _baseStats.BaseSpeed;
            _currentMovement = _currentSpeed;
        }

        /// <summary>
        /// Deals damage directly to current hitpoints.
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="attackFromAttacker"></param>
        public int TakeDamage(int baseDamage, int attackFromAttacker)
        {
            /// Merle damage formula
            //const int DAMAGE_CONST = 10;
            //int damage = Mathf.RoundToInt((baseDamage + attackFromAttacker) * (float)(DAMAGE_CONST / (float)(DAMAGE_CONST + (float)_currentDefense)));

            /// Keegan damage formula
            int totalDamage = (baseDamage + attackFromAttacker) - _currentDefense;
            int damage;
            if (totalDamage >= 0)
                damage = totalDamage;
            else
                damage = 0;

            if ((_currentHP - damage) >= 0)
                _currentHP -= damage;
            else
                _currentHP = 0;

            return damage;
        }

        /// <summary>
        /// Heals damage directly to current hitpoints.
        /// </summary>
        /// <param name="healing"></param>
        public int HealDamage(int healingAmount)
        {
            int healing = healingAmount;

            if ((_currentHP + healing) <= _baseStats.MaxHP)
                _currentHP += healing;
            else
                _currentHP = _baseStats.MaxHP;

            return healing;
        }

        /// <summary>
        /// Gains energy directly to the current energy.
        /// </summary>
        /// <param name="energyGained"></param>
        public int GainEnergy(int energyGained)
        {
            int energy = energyGained;

            if ((_currentEnergy + energy) < _baseStats.MaxEnergy)
                _currentEnergy += energy;
            else
                _currentEnergy = _baseStats.MaxEnergy;

            return energy;
        }

        /// <summary>
        /// Spends energy directly to the current energy.
        /// </summary>
        /// <param name="energySpent"></param>
        public int SpendEnergy(int energySpent)
        {
            int energy = energySpent;

            if ((_currentEnergy - energy) > 0)
                _currentEnergy -= energy;
            else
                _currentEnergy = 0;

            return energy;
        }

        /// <summary>
        /// Spends movement as the character moves
        /// </summary>
        /// <param name="movementSpent"></param>
        public int SpendMovement(int movementSpent)
        {
            int movement = movementSpent;

            if ((_currentMovement - movement) > 0)
                _currentMovement -= movement;
            else
                _currentMovement = 0;

            return movement;
        }

        /// <summary>
        /// Resets Movement to max
        /// </summary>
        public void ResetMovement()
        {
            _currentMovement = _currentSpeed;
        }

        /// <summary>
        /// Changes Defense
        /// </summary>
        public void SetDefense(int newDef)
        {
            _currentDefense = newDef;
        }

        /// <summary>
        /// Rests Defense
        /// </summary>
        public void ResetDefense()
        {
            _currentDefense = _baseStats.BaseDefense;
        }

        /// <summary>
        /// Changes Attack
        /// </summary>
        public void SetAttack(int newAtk)
        {
            _currentAttack = newAtk;
        }

        /// <summary>
        /// Resets Attack
        /// </summary>
        public void ResetAttack()
        {
            _currentAttack = _baseStats.BaseAttack;
        }

        /// <summary>
        /// Changes Speed
        /// </summary>
        public void SetSpeed(int newSpd)
        {
            _currentSpeed = newSpd;
        }

        /// <summary>
        /// Resets Speed
        /// </summary>
        public void ResetSpeed()
        {
            _currentSpeed = _baseStats.BaseSpeed;
        }

        #endregion

        #region HELPERS

        public string Name
        { get => _baseStats.Name; }

        public int MaxHP
        { get => _baseStats.MaxHP; }

        public int CurrentHP
        { get => _currentHP; }

        public int MaxEnergy
        { get => _baseStats.MaxEnergy; }

        public int CurrentEnergy
        { get => _currentEnergy; }

        public int CurrentAttack
        { get => _currentAttack; }

        public int BaseAttack
        { get => _baseStats.BaseAttack; }

        public int CurrentDefense
        { get => _currentDefense; }

        public int BaseDefense
        { get => _baseStats.BaseDefense; }

        public int CurrentSpeed
        { get => _currentSpeed; }

        public int BaseSpeed
        { get => _baseStats.BaseSpeed; }

        public int CurrentMovement
        { get => _currentMovement; }

        #endregion
    }
}