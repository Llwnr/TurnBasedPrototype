using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInflictionManager : MonoBehaviour
{
    public void InflictStatusEffects(){
        GameObject target = GetComponent<SelectTarget>().GetTarget();
        foreach(SO_StatusEffect statusEffect in GetComponent<StatusEffectsHolder>().GetStatusEffects()){
            
            //Make only unique status effects and just increase its effect count if dupes
            if(CheckForDuplicates(statusEffect.GetEffectScript(), target) && statusEffect.canStack){
                StatusEffectBase effect = target.GetComponent(System.Type.GetType(statusEffect.GetEffectScript())) as StatusEffectBase;
                effect.AddEffectCount(statusEffect.effectCount);
                continue;
            }
            //Create a new unique effect. Effects that do not stack can be created multiple times such as bombs
            StatusEffectBase effectToInflict = target.AddComponent(System.Type.GetType(statusEffect.GetEffectScript())) as StatusEffectBase;
            effectToInflict.SetActivationCondition(statusEffect.activationCondition);
            effectToInflict.SubscribeToObserver();
            effectToInflict.AddEffectCount(statusEffect.effectCount);
            effectToInflict.SetTargets(GetComponent<ReferencePlayer>().GetReferencedPlayer(), target);
            //Setting info
            effectToInflict.SetEffectInfo(statusEffect);
        }

        GetComponent<StatusEffectsHolder>().ClearAll();
    }
    //Check if the specific status effect script already exist on target, in that case just increase the effect count
    bool CheckForDuplicates(string scriptName, GameObject target){
        foreach(StatusEffectBase effectScript in target.GetComponents<StatusEffectBase>()){
            if(effectScript.GetType().Name == scriptName){
                return true;
            }
        }
        return false;
    }
}
