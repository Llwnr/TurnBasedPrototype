using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDrawback : MonoBehaviour
{
    //This will manage whether to reduce effect multiplication count based on infusion or not
    private SkillBase mySkill;
    private SkillBase.SkillType origSkillType;
    private float origCountMultiplier;
    private int origMaxAttachCount;

    public void SetSkillReference(SkillBase skillBase){
        mySkill = skillBase;
        origCountMultiplier = skillBase.GetCountMultiplier();
        origSkillType = skillBase.GetSkillType();
        origMaxAttachCount = skillBase.GetMaxStatusEffects();
    }

    public void WhenClicked(){
        //Check if user infused or not
        if(mySkill.GetSkillType() == origSkillType){
            Deactivated();
        }else{
            Activated();
        }
    }

    void Activated(){
        mySkill.SetCountMultiplier(origCountMultiplier/2f);
        mySkill.SetInfusion(true);

        //Reduce max status effects attach count
        int amtToSet = origMaxAttachCount - 2;
        if(amtToSet < 0) amtToSet = 0;
        mySkill.SetMaxStatusEffects(amtToSet);
    }
    void Deactivated(){
        mySkill.SetCountMultiplier(origCountMultiplier);
        mySkill.SetInfusion(false);
        mySkill.SetMaxStatusEffects(origMaxAttachCount);
    }
}
