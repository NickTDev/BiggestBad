// Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using T02.Characters.InBattle;
using UnityEngine;

/// <summary>
/// An effect that halves speed.
/// </summary>
public class StuckEffect : StatusEffect
{
    public StuckEffect(CharacterBattleController character, int numTurns)
    {
        Type = StatusEffectType.Stuck;
        NumTurns = numTurns;
        ApplyEffect(character);
    }

    public override void ApplyEffect(CharacterBattleController character)
    {
        character.Stats.SetSpeed(Mathf.RoundToInt(character.Stats.BaseSpeed * 0.5f));
        character.TurnManager.SortBySpeed();
    }

    public override void RemoveEffect(CharacterBattleController character)
    {
        character.Stats.ResetSpeed();
    }
}
