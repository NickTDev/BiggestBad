// Merle Roji

using T02.Characters.InBattle;
using T02.TurnBasedSystem;
using UnityEngine;

namespace T02.Characters
{
    /// <summary>
    /// Stores a party of characters.
    /// </summary>
    [CreateAssetMenu(fileName = "Character Party", menuName = "RPG System/New Character Party")]
    public class CharacterPartyManager : ScriptableObject
    {
        [SerializeField] protected CharacterBattleController[] _characterParty;
        public CharacterBattleController[] CharacterParty
        { get => _characterParty; }
    }
}
