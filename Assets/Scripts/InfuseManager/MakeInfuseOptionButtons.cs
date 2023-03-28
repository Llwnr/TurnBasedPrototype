using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class MakeInfuseOptionButtons : MonoBehaviour
{
    [SerializeField]private GameObject infuseOptionButton;//Used to change the element of the skill. Such as pierce, fire, blunt etc
    // Start is called before the first frame update
    void Start()
    {
        SetOriginalSkillType();
        CreateButtons();
    }

    void SetOriginalSkillType(){
        GetComponentInChildren<ChangeSkillDamageType>().SetSkillType(transform.parent.transform.parent.GetComponent<SkillBase>().GetSkillType());
        GetComponentInChildren<ToggleDrawback>().SetSkillReference(transform.parent.transform.parent.GetComponent<SkillBase>());
    }

    //Create the button that will change the skill damage type accordingly.
    void CreateButtons(){
        foreach(SkillBase.SkillType skillType in Enum.GetValues(typeof(SkillBase.SkillType))){
            GameObject newButton = Instantiate(infuseOptionButton, Vector3.zero, Quaternion.identity);
            newButton.transform.SetParent(transform, false);
            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skillType.ToString();
            newButton.GetComponent<ChangeSkillDamageType>().SetSkillType(skillType);
            newButton.GetComponent<ToggleDrawback>().SetSkillReference(transform.parent.transform.parent.GetComponent<SkillBase>());
        }
    }
}
