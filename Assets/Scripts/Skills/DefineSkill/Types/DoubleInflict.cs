using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleInflict : SkillBase
{
    public override void ExecuteSkillAction()
    {
        InflictStatusEffects();
        InflictStatusEffects();
    }
}
