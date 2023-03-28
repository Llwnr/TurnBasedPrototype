using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    //SET SKILL DAMAGE TYPE
    public enum SkillType{
        blunt,
        pierce,
        fire,
        water
    }
    public static SkillType skillType;
    //SET BASE DAMAGE
    private float dmgAmt;
    public void SetDmgAmt(float amt){
        dmgAmt = amt;
    }
    public float GetSkillDmgAmt(){
        return dmgAmt;
    }
    //HOW MANY STATUS EFFECTS CAN A SKILL BUTTON HOLD
    [SerializeField]private int maxStatusEffectAmount = 0;
    public void SetMaxStatusEffects(int amt){
        maxStatusEffectAmount = amt;
    }
    public int GetMaxStatusEffects(){
        return maxStatusEffectAmount;
    }
    //How much should the skill inflict status effect. x2? x1? x5?
    [SerializeField]private float countMultiplier;
    public void SetCountMultiplier(float multiplier){
        countMultiplier = multiplier;
    }
    public float GetCountMultiplier(){
        return countMultiplier;
    }
    //Should the skill activate drawbacks
    private bool isInfused = false;
    public void SetInfusion(bool value){
        isInfused = value;
    }
    //Set skill infos
    [SerializeField]private SkillType mySkillType;
    public void SetSkillType(SkillType skillType){
        mySkillType = skillType;
    }
    public SkillType GetSkillType(){
        return mySkillType;
    }
    private AttackManager attackManager;
    private void Awake() {
        attackManager = GameObject.FindWithTag("AttackManager").GetComponent<AttackManager>();
    }
    //To whom it should affect
    [SerializeField]protected GameObject attacker, target;
    public void SetTargets(GameObject attacker, GameObject target){
        this.attacker = attacker;
        this.target = target;
        AddToQueue();
    }
    public GameObject GetAttacker(){
        return attacker;
    }
    public GameObject GetTarget(){
        return target;
    }

    public void ResetTargetOnCancellation(){
        attacker = target = null;
        RemoveFromQueue();
    }

    void AddToQueue(){
        attackManager.AddToQueue(this);
    }
    void RemoveFromQueue(){
        attackManager.RemoveFromQueue(this);
    }

    protected float inflictMultiplier;
    
    public abstract void ExecuteSkillAction();

    protected void InvokeStatusEffects(){
        //Invoke status effects based on who is being hit and by whom
        StatusEffectsManager targetManager = GetTarget().GetComponent<StatusEffectsManager>();
        StatusEffectsManager attackerManager = GetAttacker().GetComponent<StatusEffectsManager>();

        

        //Get attacker's final dmg after buffs and debuffs:
        if(attacker.tag == target.tag){//If player selects player then halve the damage dealt to player
            attackerManager.SetSkillDmgAmt(GetSkillDmgAmt()/2f);
        }else{
            attackerManager.SetSkillDmgAmt(GetSkillDmgAmt());
        }
        //Drawbacks such as deal damage to self when skill is infused with another element
        ActivateDrawbacks();
        
        float attackerFinalDmg = attackerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnAttack);
        //Calculate enemy's receiving final damage after enemy's buffs and debuffs:
        targetManager.SetSkillDmgAmt(attackerFinalDmg);
        targetManager.SetAttacker(GetAttacker());
        float finalDmg = targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnHit);
        //Calculate true damage after mitigations and other effects as such
        targetManager.SetSkillDmgAmt(finalDmg);
        float trueDmg = targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnFinalDmg);
        AttackManager.CalculateDefenseAndDamage(GetTarget(), trueDmg, GetSkillType());
    }

    void ActivateDrawbacks(){
        if(!isInfused) return;
        AttackManager.CalculateDefenseAndDamage(GetAttacker(), GetSkillDmgAmt(), GetSkillType());
    }


    protected void InflictStatusEffects(){
        GetComponent<EffectInflictionManager>().InflictStatusEffects(countMultiplier);
        GetComponent<SelectTarget>().RemoveLine();
    }
}
