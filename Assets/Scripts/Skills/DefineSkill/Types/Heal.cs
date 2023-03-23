using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SkillBase
{
    public override void ExecuteSkillAction()
    {
        InvokeStatusEffectsHeal();
        InflictStatusEffects();
    }

    private void InvokeStatusEffectsHeal(){
        //Invoke status effects based on who is being hit and by whom
        StatusEffectsManager targetManager = GetTarget().GetComponent<StatusEffectsManager>();
        StatusEffectsManager attackerManager = GetAttacker().GetComponent<StatusEffectsManager>();
        //Get attacker's final dmg after buffs and debuffs:
        attackerManager.SetSkillDmgAmt(-GetSkillDmgAmt());
        
        float attackerFinalDmg = attackerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnAttack);
        //Calculate enemy's receiving final damage after enemy's buffs and debuffs:
        targetManager.SetSkillDmgAmt(attackerFinalDmg);
        targetManager.SetAttacker(GetAttacker());
        float finalDmg = targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnHit);
        //Calculate true damage after mitigations and other effects as such
        targetManager.SetSkillDmgAmt(finalDmg);
        float trueDmg = targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnFinalDmg);
        target.GetComponent<HealthManager>().DamagePlayerBy(trueDmg);
        Debug.Log(trueDmg);
    }
}
