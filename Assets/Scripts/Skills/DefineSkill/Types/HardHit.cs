using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHit : SkillBase
{
    public override void ExecuteSkillAction()
    {
        InvokeStatusEffects();
        InflictStatusEffects();
    }
}
