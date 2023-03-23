using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rupture : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        GetComponent<HealthManager>().DamagePlayerBy(effectCount);
        ReduceCount();
        return dmgAmt;
    }
}
