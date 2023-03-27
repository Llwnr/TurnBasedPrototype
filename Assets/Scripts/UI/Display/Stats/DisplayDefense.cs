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
        for(int i=0; i<4; i++){
            CreatePanel(i);
        }
    }

    void CreatePanel(int defenseType){
        GameObject newDefensePanel = Instantiate(defensePanel, Vector3.zero, Quaternion.identity);
        //Set its data
        float defenseTypeIndex = defenseManager.GetDefenseOfType((SkillBase.SkillType)defenseType);
        string defenseAdv = CalculateDefenseAdvantageType(defenseTypeIndex) + " to " + ((SkillBase.SkillType)defenseType).ToString();
        newDefensePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = defenseAdv;
        newDefensePanel.GetComponent<Image>().sprite = defenseManager.GetDefenseIcon((SkillBase.SkillType)defenseType);

        newDefensePanel.transform.SetParent(transform,false);
    }

    string CalculateDefenseAdvantageType(float defenseValue){
        if(defenseValue >= 1.5) return "Endured";
        else if(defenseValue >= 1) return "Normal";
        else if(defenseValue >= 0.5) return "Weak";
        else return "Allergic";
    }
}
