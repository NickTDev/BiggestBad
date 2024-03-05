// Keegan Buckingham, Merle Roji

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace T02.Characters
{
    /// <summary>
    /// Saves all Character stat blocks into a scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterData", menuName = "RPG System/New Character Data Set", order = 0)]
    public class CharacterDataManager : ScriptableObject
    {
        [SerializeField] private string _file;

        [SerializeField] private List<Character> _characters = new List<Character>();

        public void LoadCharacters()
        {
            _characters.Clear();
            List<Dictionary<string, object>> data = CSVReader.Read(_file);

            for (var i = 0; i < data.Count; i++)
            {
                int id = int.Parse(data[i]["id"].ToString(), System.Globalization.NumberStyles.Integer);
                string name = data[i]["Name"].ToString();
                int hitPoints = int.Parse(data[i]["HitPoints"].ToString(), System.Globalization.NumberStyles.Integer);
                int energyPoints = int.Parse(data[i]["EnergyPoints"].ToString(), System.Globalization.NumberStyles.Integer);
                int attack = int.Parse(data[i]["Attack"].ToString(), System.Globalization.NumberStyles.Integer);
                int defense = int.Parse(data[i]["Defense"].ToString(), System.Globalization.NumberStyles.Integer);
                int speed = int.Parse(data[i]["Speed"].ToString(), System.Globalization.NumberStyles.Integer);
                string movementType = data[i]["MovementType"].ToString();

                /*
                print("id " + data[i]["id"] + " " +
                       "Name " + data[i]["Name"] + " " +
                       "HitPoints " + data[i]["HitPoints"] + " " +
                       "Attack " + data[i]["Attack"] + " " +
                       "Defense " + data[i]["Defense"] + " " +
                       "Movement " + data[i]["Movement"] + " " +
                       "MovementType " + data[i]["MovementType"]);
                */

                AddItem(id, name, hitPoints, energyPoints, attack, defense, speed, movementType);
            }

        }

        private void AddItem(int id, string name, int hitPoints, int energyPoints, int attack, int defense,
            int speed, string movementType)
        {
            Character tempCharacter = new Character();

            tempCharacter.ID = id;
            tempCharacter.Name = name;
            tempCharacter.HitPoints = hitPoints;
            tempCharacter.EnergyPoints = energyPoints;
            tempCharacter.Attack = attack;
            tempCharacter.Defense = defense;
            tempCharacter.Speed = speed;
            tempCharacter.MovementType = movementType;

            _characters.Add(tempCharacter);
        }

        #if UNITY_EDITOR
        public void UpdateCharacterStatObjects()
        {
            // save paths
            string basePath = "Assets/Resources/Character Data";
            string basePlayerPath = basePath + "/Players";
            string baseEnemyPath = basePath + "/Enemies";

            foreach (Character character in _characters) // loop through all characters
            {
                // check if the character object already exists
                // check if the character object is a player or an enemy
                CharacterStats existingCharacter;
                if (character.ID <= 4)
                    existingCharacter = Resources.Load("Character Data/Players/" + character.Name) as CharacterStats;
                else
                    existingCharacter = Resources.Load("Character Data/Enemies/" + character.Name) as CharacterStats;

                bool characterExists = existingCharacter != null;
                if (characterExists)
                {
                    // inject character stats into existing character object
                    existingCharacter.InjectStats
                        (
                        character.Name, // stat name
                        character.HitPoints,
                        character.EnergyPoints,
                        character.Attack,
                        character.Defense,
                        character.Speed,
                        character.MovementType
                        );
                }
                else
                {
                    // inject character stats into a new scriptable object instance
                    CharacterStats characterStatObject = ScriptableObject.CreateInstance<CharacterStats>();
                    characterStatObject.name = character.Name; // file name
                    characterStatObject.InjectStats
                        (
                        character.Name, // stat name
                        character.HitPoints,
                        character.EnergyPoints,
                        character.Attack,
                        character.Defense,
                        character.Speed,
                        character.MovementType
                        );

                    // create object in project files
                    string finalPath;
                    if (character.ID <= 4)
                        finalPath = basePlayerPath + "/" + character.Name + ".asset";
                    else
                        finalPath = baseEnemyPath + "/" + character.Name + ".asset";


                    AssetDatabase.CreateAsset(characterStatObject, finalPath);
                }
            }
        }
        #endif
    }
}
