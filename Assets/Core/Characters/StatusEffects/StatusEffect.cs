// Nicholas Tvaroha

using System;
using System.Collections;
using System.Collections.Generic;
using T02.Characters;
using T02.Characters.InBattle;
using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// List of Status Effect Types
    /// </summary>
    public enum StatusEffectType
    {
        None,
        Stuck,
        Vulnerable
    }

    /// <summary>
    /// Status Effect Template
    /// </summary>
    [Serializable]
    public class StatusEffect
    {
        public StatusEffectType Type; //The type of effect
        public int NumTurns; //The number of turns the effect lasts for, decreasing at the end of a character's turn

        public virtual void ApplyEffect(CharacterBattleController character) { } //Applies the effects of the status condition
        public virtual void RemoveEffect(CharacterBattleController character) { } //Removes the effects of the status condition
    }
}