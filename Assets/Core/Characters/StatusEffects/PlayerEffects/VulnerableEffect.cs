// Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using T02.Characters.InBattle;
using UnityEngine;

/// <summary>
/// An effect that increases the amount of damage a character takes
/// </summary>
public class VulnerableEffect : StatusEffect
{
    public VulnerableEffect(CharacterBattleController character, int numTurns)
    {
        Type = StatusEffectType.Vulnerable;
        NumTurns = numTurns;
        ApplyEffect(character);
    }

    public override void ApplyEffect(CharacterBattleController character)
    {
        character.Stats.SetDefense((int)(character.Stats.BaseDefense * 0.5f));
    }

    public override void RemoveEffect(CharacterBattleController character)
    {
        character.Stats.ResetDefense();
    }
}
