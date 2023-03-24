using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetEffectInfo : MonoBehaviour
{
    [SerializeField]private GameObject canvas;
    [SerializeField]private GameObject effectCount;
    [SerializeField]private GameObject descBox;

    public void SetCanvasSortOrder(int index){
        canvas.GetComponent<Canvas>().sortingOrder = index;
    }

    public void SetEffectCount(string text){
        effectCount.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetDescription(string nameText, string descText){
        string mainText = "<size=110%><b><color=#FF0000>" + nameText + "</color></b></size>" + "\n" + descText;
        descBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = mainText;
    }
}
