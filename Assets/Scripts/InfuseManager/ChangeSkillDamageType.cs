using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkillDamageType : MonoBehaviour
{
    private SkillBase.SkillType skillType;
    public void SetSkillType(SkillBase.SkillType skillType){
        this.skillType = skillType;
    }
    public void ChangeSkill(){
        transform.parent.transform.parent.transform.parent.GetComponent<SkillBase>().SetSkillType(skillType);
    }
}
