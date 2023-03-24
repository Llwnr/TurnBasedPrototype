using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        dmgAmt -= effectCount;
        if(dmgAmt < 0) dmgAmt = 0;
        ReduceCount();
        return dmgAmt;
    }

    public override void ReduceCount()
    {
        effectCount--;
        if(effectCount <= 0){
            GetComponent<HealthManager>().DamagePlayerBy(20);
            StartCoroutine(DestroySelf());
        }
    }
}
