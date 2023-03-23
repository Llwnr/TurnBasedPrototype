using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class DisplayEffectsOnPlayers : MonoBehaviour
{
    [SerializeField]private List<StatusEffectBase> statusEffectBases = new List<StatusEffectBase>();
    [SerializeField]private List<GameObject> effectIcons = new List<GameObject>();

    [SerializeField]private GameObject effectDisplayContainer;
    [SerializeField]private GameObject effectIcon;

    private void Awake() {
        statusEffectBases = GetComponents<StatusEffectBase>().ToList();
    }
    
    private void Update() {
        DisplayStatusEffects();
    }

    void DisplayStatusEffects(){
        StatusEffectBase[] updatedStatusEffectBases = GetComponents<StatusEffectBase>();
        for(int i=0; i<effectIcons.Count; i++){
            effectIcons[i].GetComponent<SetEffectInfo>().SetEffectCount(statusEffectBases[i].GetEffectCount().ToString());
        }
        //If there is deletion or addition of a status effect  
        //For Addition
        for(int i=0; i<updatedStatusEffectBases.Length; i++){
            if(!statusEffectBases.Contains(updatedStatusEffectBases[i])){
                statusEffectBases.Add(updatedStatusEffectBases[i]);
                effectIcons.Add(DisplayEffect(updatedStatusEffectBases[i]));
            }
        }
        //For Deletion
        for(int i=0; i<statusEffectBases.Count; i++){
            if(!updatedStatusEffectBases.Contains(statusEffectBases[i])){
                statusEffectBases.RemoveAt(i);
                Destroy(effectIcons[i].gameObject);
                effectIcons.RemoveAt(i);
                i--;
            }
        }
    }

    GameObject DisplayEffect(StatusEffectBase statusEffect){
        SO_StatusEffect effectData = statusEffect.GetEffectInfo();

        GameObject newEffectIcon = Instantiate(effectIcon, Vector3.zero, Quaternion.identity);
        newEffectIcon.transform.SetParent(effectDisplayContainer.transform, false);
        newEffectIcon.GetComponent<Image>().sprite = effectData.effectIcon;
        newEffectIcon.GetComponent<SetEffectInfo>().SetEffectCount(effectData.effectCount.ToString());
        newEffectIcon.GetComponent<SetEffectInfo>().SetDescription(effectData.effectName + "\n" + effectData.description);
        return newEffectIcon;
    }
}
