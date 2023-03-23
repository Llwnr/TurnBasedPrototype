using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefUp : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        dmgAmt /= (1+effectCount/10f);
        ReduceCount();
        return dmgAmt;
    }
}
