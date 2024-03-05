//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using T02.Characters;
using UnityEngine;

public enum ConditionType
{
    NONE,
    SANDY,
    HURT,
    HEAL
}

public class GridCondition
{
    public ConditionType conditionType;

    /// <summary>
    /// Virtual function for all children conditions
    /// </summary>
    public virtual void triggerCondition(CharacterStats character) { }
}
