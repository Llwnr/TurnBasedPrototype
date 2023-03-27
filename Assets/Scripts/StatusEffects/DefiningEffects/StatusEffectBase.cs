using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectBase : MonoBehaviour
{   public enum ActivationCondition{
        OnAttack,
        OnHit,
        OnTurnEnd,
        DOT,
        OnFinalDmg
    }
    public static ActivationCondition condition;
    //Effect type. Is it a buff or debuff?
    public enum EffectType{
        buff,
        debuff
    }
    public EffectType effectType;

    //Manage when to activate the status effects
    protected ActivationCondition activationCondition;
    public virtual ActivationCondition GetActivationCondition(){
        return activationCondition;
    }
    public void SetActivationCondition(ActivationCondition activationCondition){
        this.activationCondition = activationCondition;
    }

    //GET SKILL INFO
    protected SO_StatusEffect statusEffectData;
    public void SetEffectInfo(SO_StatusEffect statusEffectData){
        this.statusEffectData = statusEffectData;
    }
    public SO_StatusEffect GetEffectInfo(){
        return statusEffectData;
    }
    public int GetEffectCount(){
        return effectCount;
    }

    //Manipulate the damage
    protected float skillDmg;
    [SerializeField]protected int effectCount = 0;
    public void SetSkillDamage(float dmgAmt){
        skillDmg = dmgAmt;
    }
    public void AddEffectCount(int count){
        effectCount += count;
    }

    //Manage targets
    [SerializeField]protected GameObject inflicter, target;
    //Inflicter is the one who inflicts, attacker is the one who attacks
    public void SetTargets(GameObject inflicter, GameObject target){
        this.inflicter = inflicter;
        this.target = target;
    }

    public abstract float ExecuteEffectsAction(float dmgAmt);
    public virtual void ReduceCount(){
        effectCount = Mathf.FloorToInt(0.4f*effectCount);
        if(effectCount<=0){
            StartCoroutine(DestroySelf());
        }
    }

    protected IEnumerator DestroySelf(){
        yield return null;
        if(effectCount<=0){
            Destroy(this);
        }
    }

    //MANAGE OBSERVERS
    public void SubscribeToObserver() {
        GetComponent<O_StatusEffect>().AddObserver(this);
    }

    private void OnDestroy() {
        GetComponent<O_StatusEffect>().RemoveObserver(this);
    }
}
