using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetSkillInfo : MonoBehaviour
{
    [SerializeField]private GameObject nameText;
    [SerializeField]private GameObject dmgText;
    [SerializeField]private GameObject descBox;

    public void SetName(string text){
        nameText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetDamageText(string text){
        dmgText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetDescription(string text){
        descBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }
    

}
