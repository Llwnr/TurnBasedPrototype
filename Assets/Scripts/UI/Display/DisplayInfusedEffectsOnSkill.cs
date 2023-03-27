using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfusedEffectsOnSkill : MonoBehaviour
{
    [SerializeField]private GameObject effectContainer, effectIcon;
    private List<GameObject> effectIcons = new List<GameObject>();
    public void CreateEffectIcon(SO_StatusEffect statusEffect){
        GameObject newEffectIcon = Instantiate(effectIcon, Vector3.zero, Quaternion.identity);
        newEffectIcon.transform.SetParent(effectContainer.transform, false);
        newEffectIcon.GetComponent<Image>().sprite = statusEffect.effectIcon;

        SetEffectInfo iconInfo = newEffectIcon.GetComponent<SetEffectInfo>();
        iconInfo.SetEffectCount(statusEffect.effectCount.ToString());
        iconInfo.SetDescription(statusEffect.effectName,statusEffect.description);
        iconInfo.SetColor(statusEffect);
        effectIcons.Add(newEffectIcon);
    }

    private void Update() {
        StatusEffectsHolder effectsHolder = GetComponent<StatusEffectsHolder>();
        if(effectsHolder == null) return;

        RemoveExcessEffects(effectsHolder);
        //Check if the effects attacked to skills are removed
        for(int i=0; i<effectsHolder.GetStatusEffects().Count; i++){
            //Remove null elements
            if(effectsHolder.GetStatusEffects()[i] == null){
                effectsHolder.GetStatusEffects().RemoveAt(i);
                i--;
            }
        }
        
        //Destroy icons that have been removed
        if(GetComponent<StatusEffectsHolder>().GetStatusEffects().Count == 0){
            DestroyIcons();
            effectIcons.Clear();
        }
    }

    public void DestroyIcons(){
        foreach(GameObject icon in effectIcons){
            Destroy(icon.gameObject);
        }
    }

    void RemoveExcessEffects(StatusEffectsHolder effectsHolder){
        if(effectsHolder.GetStatusEffects().Count != effectIcons.Count){
            Destroy(effectIcons[effectIcons.Count-1]);
            effectIcons.RemoveAt(effectIcons.Count-1);
        }
    }
}
