using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkill : SkillBase
{
    public override void ExecuteSkillAction()
    {
        InvokeStatusEffects();
        InflictStatusEffects();
    }
}
