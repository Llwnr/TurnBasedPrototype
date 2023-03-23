using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkUp : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        dmgAmt += (effectCount/10f)*dmgAmt;
        ReduceCount();
        return dmgAmt;
    }
}
