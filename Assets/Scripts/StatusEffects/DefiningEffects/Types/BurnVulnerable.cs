using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnVulnerable : StatusEffectBase
{
    //DOES NOT NEED TO DO ANYTHING. IT'S EFFECT COUNT IS TAKEN AS DAMAGE BUFF BY BURN STATUS EFFECT
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        return dmgAmt;
    }
}
