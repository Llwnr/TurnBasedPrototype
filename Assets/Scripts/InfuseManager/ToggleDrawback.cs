using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDrawback : MonoBehaviour
{
    //This will manage whether to reduce effect multiplication count based on infusion or not
    private SkillBase mySkill;
    private SkillBase.SkillType origSkillType;
    private float origCountMultiplier;

    public void SetSkillReference(SkillBase skillBase){
        mySkill = skillBase;
        origCountMultiplier = skillBase.GetCountMultiplier();
        origSkillType = skillBase.GetSkillType();
    }

    public void WhenClicked(){
        //Check if user infused or not
        if(mySkill.GetSkillType() == origSkillType){
            mySkill.SetInfusion(false);
            Deactivated();
        }else{
            mySkill.SetInfusion(true);
            Activated();
        }
    }

    void Activated(){
        mySkill.SetCountMultiplier(origCountMultiplier/2f);
    }
    void Deactivated(){
        mySkill.SetCountMultiplier(origCountMultiplier);
    }
}
