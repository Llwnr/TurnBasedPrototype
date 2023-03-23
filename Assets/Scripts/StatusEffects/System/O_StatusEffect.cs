using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class O_StatusEffect : MonoBehaviour
{
    public StatusEffectBase.ActivationCondition testCondition;
    private float skillDmgAmt;
    public void SetSkillDmgAmt(float skillDmgAmt){
        this.skillDmgAmt = skillDmgAmt;
    }
    //ONLY ONE CLASS PER GAMEOBJECT
    private void OnEnable() {
        if(GetComponents<O_StatusEffect>().Length > 1) Debug.LogError("More than one status effect observer");
    }

    //The one who attacks
    private GameObject attacker;
    public void SetAttacker(GameObject attacker){
        this.attacker = attacker;
    }
    public GameObject GetAttacker(){
        return attacker;
    }

    [SerializeField]private List<StatusEffectBase> onAttackAction = new List<StatusEffectBase>();
    [SerializeField]private List<StatusEffectBase> onHitAction = new List<StatusEffectBase>();
    [SerializeField]private List<StatusEffectBase> onDamageOverTimeAction = new List<StatusEffectBase>();
    [SerializeField]private List<StatusEffectBase> onDeathAction = new List<StatusEffectBase>();
    [SerializeField]private List<StatusEffectBase> turnEndAction = new List<StatusEffectBase>();
    //When you receive the damage after calculating all other status effects, activate these first; Like, dmg mitigation in gbf
    [SerializeField]private List<StatusEffectBase> onFinalDmgReceivedAction = new List<StatusEffectBase>();

    private List<StatusEffectBase> statusEffects = new List<StatusEffectBase>();

    //Manage observers
    public virtual void AddObserver(StatusEffectBase effectAbility){
        statusEffects.Add(effectAbility);
        switch(effectAbility.GetActivationCondition()){
            case StatusEffectBase.ActivationCondition.OnAttack:
                onAttackAction.Add(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.OnHit:
                onHitAction.Add(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.DOT:
                onDamageOverTimeAction.Add(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.OnTurnEnd:
                turnEndAction.Add(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.OnFinalDmg:
                onFinalDmgReceivedAction.Add(effectAbility);
                break;
            default:
                break;
        }
    }

    public virtual void RemoveObserver(StatusEffectBase effectAbility){
        statusEffects.Remove(effectAbility);
        switch(effectAbility.GetActivationCondition()){
            case StatusEffectBase.ActivationCondition.OnAttack:
                onAttackAction.Remove(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.OnHit:
                onHitAction.Remove(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.DOT:
                onDamageOverTimeAction.Remove(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.OnTurnEnd:
                turnEndAction.Remove(effectAbility);
                break;
            case StatusEffectBase.ActivationCondition.OnFinalDmg:
                onFinalDmgReceivedAction.Remove(effectAbility);
                break;
            default:
                break;
        }
    }


    //This will handle what sort of status effects to execute based on the state of the character. Is the character attacking?
    //If player attacks with 6 dmg, first his dmg after buffs, say 50% becomes 9, then it is calculated onto enemy with debuffs
    public float GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition stateOfStatusEffect){
        testCondition = stateOfStatusEffect;
        switch(stateOfStatusEffect){
            case StatusEffectBase.ActivationCondition.OnAttack:
                return ExecuteEffectsFrom(onAttackAction);
            case StatusEffectBase.ActivationCondition.OnHit:
                return ExecuteEffectsFrom(onHitAction);
            case StatusEffectBase.ActivationCondition.DOT:
                return ExecuteEffectsFrom(onDamageOverTimeAction);
            case StatusEffectBase.ActivationCondition.OnTurnEnd:
                return ExecuteEffectsFrom(turnEndAction);
            case StatusEffectBase.ActivationCondition.OnFinalDmg:
                return ExecuteEffectsFrom(onFinalDmgReceivedAction);
            default:
                break;
        }
        return 0;
    }

    float ExecuteEffectsFrom(List<StatusEffectBase> statusEffectsToExecute){
        //Goes through all the status effects that activated to give the final output
        float baseDmg = skillDmgAmt;
        foreach(StatusEffectBase statusEffect in statusEffectsToExecute){
            baseDmg = statusEffect.ExecuteEffectsAction(baseDmg);
        }
        return baseDmg;
    }
}
