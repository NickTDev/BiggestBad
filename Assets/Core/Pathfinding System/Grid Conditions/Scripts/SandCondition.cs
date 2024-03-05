//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using T02.Characters;
using UnityEngine;

public class SandCondition : GridCondition
{
    public SandCondition()
    {
        conditionType = ConditionType.SANDY;
    }

    /// <summary>
    /// Reduce the character's speed by twice the normal amount
    /// </summary>
    public override void triggerCondition(CharacterStats character)
    {
        
    }
}
