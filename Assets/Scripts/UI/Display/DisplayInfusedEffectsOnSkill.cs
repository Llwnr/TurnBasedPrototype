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
        newEffectIcon.GetComponent<SetEffectInfo>().SetEffectCount(statusEffect.effectCount.ToString());
        newEffectIcon.GetComponent<SetEffectInfo>().SetDescription(statusEffect.effectName + "\n" + statusEffect.description);
        effectIcons.Add(newEffectIcon);
    }

    private void Update() {
        if(GetComponent<StatusEffectsHolder>().GetStatusEffects().Count == 0){
            foreach(GameObject icon in effectIcons){
                Destroy(icon.gameObject);
            }
            effectIcons.Clear();
        }
    }
}
