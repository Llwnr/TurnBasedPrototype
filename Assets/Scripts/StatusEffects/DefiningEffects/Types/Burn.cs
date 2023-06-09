﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        float multiplier = 1;
        //If vulnerable exist, enhance burn damage based on vulnerability effect count
        foreach(StatusEffectBase statusEffect in GetComponents<StatusEffectBase>()){
            if(statusEffect.GetType().Name == "BurnVulnerable"){
                multiplier = 1 + statusEffect.GetEffectCount()/10f;
                statusEffect.ReduceCount();
            }
        }
        float baseDmgAmt = effectCount*multiplier;
        GetComponent<HealthManager>().DamagePlayerBy(CalculateDmgAfterElementalAdvantage(baseDmgAmt));
        ReduceCount();
        return dmgAmt;
    }

    float CalculateDmgAfterElementalAdvantage(float dmgAmt){
        float fireDef = GetComponent<DefenseManager>().GetFireDef();
        dmgAmt = dmgAmt/fireDef;
        return dmgAmt;
    }
}
