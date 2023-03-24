using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : StatusEffectBase
{
    public override float ExecuteEffectsAction(float dmgAmt)
    {
        //Don't activate reflect on hit incase the on hit was resulted by status effects such as poison, burn etc
        if(GetComponent<StatusEffectsManager>().GetAttacker() == null) return dmgAmt;
        //When reflecting damage, also provide info about who is reflecting damage by setting it as attacker
        //For example, if p1 has reflect, when reflect dmg is activated, set the enemy's attacker as p1
        GetComponent<StatusEffectsManager>().GetAttacker().GetComponent<StatusEffectsManager>().SetAttacker(gameObject);

        GetComponent<StatusEffectsManager>().GetAttacker().GetComponent<HealthManager>().DamagePlayerBy(dmgAmt*effectCount*0.1f);
        ReduceCount();
        return dmgAmt;
    }
}
