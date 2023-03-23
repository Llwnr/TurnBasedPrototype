using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiHit : SkillBase
{
    public override void ExecuteSkillAction()
    {
        InvokeStatusEffects();
        InvokeStatusEffects();
        InflictStatusEffects();
    }
}
