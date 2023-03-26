using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetSkillDmgTypeText : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Set the text to show what type of damage the skill will do
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<SkillBase>().GetSkillType().ToString();
    }
}
