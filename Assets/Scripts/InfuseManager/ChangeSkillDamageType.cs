using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkillDamageType : MonoBehaviour
{
    private SkillBase.SkillType skillType;
    private SkillBase.SkillType origSkillType;
    //Activates once only, when button is created
    public void SetSkillType(SkillBase.SkillType skillType){
        this.skillType = skillType;
        origSkillType = skillType;
    }
    //Activates when clicked
    public void ChangeSkill(){
        transform.parent.transform.parent.transform.parent.GetComponent<SkillBase>().SetSkillType(skillType);
    }
    public void ResetSkillType(){
        transform.parent.transform.parent.transform.parent.GetComponent<SkillBase>().SetSkillType(skillType);
    }
}
