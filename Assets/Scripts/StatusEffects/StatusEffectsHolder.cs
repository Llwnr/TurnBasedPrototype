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

    public void ClearAll(){
        myStatusEffects.Clear();
    }
}
