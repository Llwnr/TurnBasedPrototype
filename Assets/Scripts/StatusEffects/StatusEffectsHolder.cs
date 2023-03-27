using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsHolder : MonoBehaviour
{
    [SerializeField]private List<SO_StatusEffect> myStatusEffects = new List<SO_StatusEffect>();

    public List<SO_StatusEffect> GetStatusEffects(){
        return myStatusEffects;
    } 

    public void InfuseEffect(SO_StatusEffect statusEffect){
        myStatusEffects.Add(statusEffect);
        if(GetComponent<DisplayInfusedEffectsOnSkill>()){
            GetComponent<DisplayInfusedEffectsOnSkill>().CreateEffectIcon(statusEffect);
        }
    }

    public void OnInfuse() {
        SkillBase mySkill = GetComponent<SkillBase>();
        if(mySkill == null) return;
        //Reduce skill holding amount when infused
        int maxHoldAmt = mySkill.GetMaxStatusEffects();
        maxHoldAmt -= 2;
        if(maxHoldAmt < 0) maxHoldAmt = 0;
        mySkill.SetMaxStatusEffects(maxHoldAmt);
        Debug.Log(maxHoldAmt);
    }

    private void Update() {
        SkillBase mySkill = GetComponent<SkillBase>();
        if(mySkill == null) return;

        while(myStatusEffects.Count > mySkill.GetMaxStatusEffects()){
            myStatusEffects.RemoveAt(myStatusEffects.Count-1);
        }
    }

    public void ClearAll(){
        myStatusEffects.Clear();
    }
}
