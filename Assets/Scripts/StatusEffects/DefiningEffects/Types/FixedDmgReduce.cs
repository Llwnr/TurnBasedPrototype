using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedDmgReduce : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        Debug.Log("reduce fixed dmg");
        dmgAmt -= effectCount;
        if(dmgAmt < 0) dmgAmt = 0;
        ReduceCount();
        return dmgAmt;
    }
}
