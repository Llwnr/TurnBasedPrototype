using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayDefense : MonoBehaviour
{
    [SerializeField]private DefenseManager defenseManager;
    [SerializeField]private GameObject defensePanel;
    
    private void Awake() {
        SetDefenseInfo();
    }

    void SetDefenseInfo(){
        for(int i=0; i<5; i++){
            CreatePanel(i);
        }
    }

    void CreatePanel(int defenseType){
        GameObject newDefensePanel = Instantiate(defensePanel, Vector3.zero, Quaternion.identity);
        //Set its data
        newDefensePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = defenseManager.GetDefenseOfType((SkillBase.SkillType)defenseType).ToString();
        newDefensePanel.GetComponent<Image>().sprite = defenseManager.GetDefenseIcon((SkillBase.SkillType)defenseType);

        newDefensePanel.transform.SetParent(transform,false);
    }
}
