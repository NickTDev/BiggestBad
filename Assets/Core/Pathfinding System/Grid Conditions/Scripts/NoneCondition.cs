//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using T02.Characters;
using UnityEngine;

public class NoneCondition : GridCondition
{
    public NoneCondition()
    {
        conditionType = ConditionType.NONE;
    }

    /// <summary>
    /// Empty Condition
    /// </summary>
    public override void triggerCondition(CharacterStats character)
    {

    }
}
