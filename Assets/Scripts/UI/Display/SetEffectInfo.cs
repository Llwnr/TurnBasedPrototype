using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetEffectInfo : MonoBehaviour
{
    [SerializeField]private GameObject effectCount;
    [SerializeField]private GameObject descBox;

    public void SetEffectCount(string text){
        effectCount.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetDescription(string text){
        descBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }
}
