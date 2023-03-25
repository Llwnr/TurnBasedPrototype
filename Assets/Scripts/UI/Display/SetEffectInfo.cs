using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetEffectInfo : MonoBehaviour
{
    [SerializeField]private GameObject effectCount;
    [SerializeField]private GameObject nameOnlyTMPro;
    [SerializeField]private GameObject descBox;
    [SerializeField]private bool showOnlyName;

    [SerializeField]private Color32 buffColor, debuffColor;

    //Set icon color based on buff or debuff
    public void SetColor(SO_StatusEffect statusEffect){
        Image imageColor = GetComponent<Image>();
        switch(statusEffect.effectType){
            case StatusEffectBase.EffectType.buff:
                imageColor.color = buffColor;
                break;
            case StatusEffectBase.EffectType.debuff:
                imageColor.color = debuffColor;
                break;
            default:
                break;
        }
    }

    public void SetEffectCount(string text){
        effectCount.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetDescription(string nameText, string descText){
        if(showOnlyName){
            SetName(nameText);
            SetDescOnly(descText);
            return;
        }
        string mainText = "<size=80%><b><color=#FF0000>" + nameText + "</color></b></size>" + "\n" + "<size=50%>" + descText + "</size>";
        descBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mainText;
    }

    void SetName(string nameText){
        nameOnlyTMPro.GetComponent<TextMeshProUGUI>().text = "<size=80%><b><color=#FF0000>" + nameText + "</color></b></size>";
    }

    void SetDescOnly(string descText){
        descBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<size=80%>" + descText + "</size>";
    }
}
