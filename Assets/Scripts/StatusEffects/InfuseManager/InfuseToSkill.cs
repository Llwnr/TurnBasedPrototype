using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfuseToSkill : MonoBehaviour
{
    [SerializeField]private SO_StatusEffect statusEffect;
    public void SetStatusEffect(SO_StatusEffect statusEffect){
        this.statusEffect = statusEffect;
    }

    public void Infuse(){
        GameObject skillBtn = transform.parent.GetComponent<ReferenceSkill>().GetReferencedSkillButton();
        int currentStatusEffectsCount = skillBtn.GetComponent<StatusEffectsHolder>().GetStatusEffects().Count;
        int maxLimit = skillBtn.GetComponent<SkillBase>().GetMaxStatusEffects();
        if(currentStatusEffectsCount >= maxLimit){
            Debug.Log("Skill btn already full with effects");
            return;
        }
        skillBtn.GetComponent<StatusEffectsHolder>().InfuseEffect(statusEffect);
    }
}
