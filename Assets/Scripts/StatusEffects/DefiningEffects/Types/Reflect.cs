using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        GetComponent<StatusEffectsManager>().GetAttacker().GetComponent<HealthManager>().DamagePlayerBy(dmgAmt*effectCount*0.1f);
        ReduceCount();
        return dmgAmt;
    }
}
