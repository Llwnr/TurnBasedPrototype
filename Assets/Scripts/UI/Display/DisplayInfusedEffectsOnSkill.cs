using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfusedEffectsOnSkill : MonoBehaviour
{
    [SerializeField]private GameObject effectContainer, effectIcon;
    private List<GameObject> effectIcons = new List<GameObject>();
    private int sortIndex = 10;
    public void CreateEffectIcon(SO_StatusEffect statusEffect){
        GameObject newEffectIcon = Instantiate(effectIcon, Vector3.zero, Quaternion.identity);
        newEffectIcon.transform.SetParent(effectContainer.transform, false);
        newEffectIcon.GetComponent<Image>().sprite = statusEffect.effectIcon;

        SetEffectInfo iconInfo = newEffectIcon.GetComponent<SetEffectInfo>();
        iconInfo.SetEffectCount(statusEffect.effectCount.ToString());
        iconInfo.SetDescription(statusEffect.effectName + "\n" + statusEffect.description);
        iconInfo.SetCanvasSortOrder(sortIndex);
        sortIndex--;
        effectIcons.Add(newEffectIcon);
    }

    private void Update() {
        if(GetComponent<StatusEffectsHolder>() != null && GetComponent<StatusEffectsHolder>().GetStatusEffects().Count == 0){
            DestroyIcons();
        }
    }

    public void DestroyIcons(){
        foreach(GameObject icon in effectIcons){
            Destroy(icon.gameObject);
        }
        effectIcons.Clear();
        sortIndex = 10;
    }
}
