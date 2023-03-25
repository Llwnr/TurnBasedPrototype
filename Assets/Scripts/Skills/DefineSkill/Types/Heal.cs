using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SkillBase
{
    public override void ExecuteSkillAction()
    {
        GetTarget().GetComponent<HealthManager>().DamagePlayerBy(-GetSkillDmgAmt());
        InflictStatusEffects();
    }
}
