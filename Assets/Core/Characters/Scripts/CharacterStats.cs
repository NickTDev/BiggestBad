// Merle Roji, Keegan Buckingham, Nicholas Tvaroha

using UnityEngine;

namespace T02.Characters
{
    /// <summary>
    /// Stores the character's stats.
    /// </summary>
    //[CreateAssetMenu(fileName = "New Character Stats", menuName = "RPG System/Character/Create Stat Block", order = 0)]
    public class CharacterStats : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private string _name = "Name";
        [SerializeField, Min(1)] private int _maxHP = 1;
        [SerializeField, Min(1)] private int _maxEnergy = 1;
        [SerializeField, Min(1)] private int _baseAttack = 1;
        [SerializeField, Min(1)] private int _baseDefense = 1;
        [SerializeField, Min(1)] private int _baseSpeed = 1;
        [SerializeField] private string _movementType = "Ground";

        #endregion

        #region STAT INJECTION

        /// <summary>
        /// Injects the stats.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hitPoints"></param>
        /// <param name="energyPoints"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="speed"></param>
        /// <param name="movementType"></param>
        public void InjectStats(string name, int hitPoints, int energyPoints, int attack, int defense, int speed, string movementType)
        {
            _name = name;
            _maxHP = hitPoints;
            _maxEnergy = energyPoints;
            _baseAttack = attack;
            _baseDefense = defense;
            _baseSpeed = speed;
            _movementType = movementType;
        }

        #endregion

        #region STAT CALCULATIONS

        #endregion

        #region HELPERS

        public string Name
        { get => _name; }

        public int MaxHP
        { get => _maxHP; }

        public int MaxEnergy
        { get => _maxEnergy; }

        public int BaseAttack
        { get => _baseAttack; }

        public int BaseDefense
        { get => _baseDefense; }

        public int BaseSpeed
        { get => _baseSpeed; }

        #endregion
    }
}
