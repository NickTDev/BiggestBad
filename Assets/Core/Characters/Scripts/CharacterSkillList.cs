// Merle Roji

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.Characters
{
    /// <summary>
    /// Stores a character's list of skills.
    /// </summary>
    [CreateAssetMenu(fileName = "Skill List", menuName = "RPG System/New Skill List")]
    public class CharacterSkillList : ScriptableObject
    {
        [SerializeField] private MinigameStateMachine[] _skills;

        public MinigameStateMachine[] Skills
        { get => _skills; }
    }
}