using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseManager : MonoBehaviour
{
    [SerializeField]private float bluntDef, pierceDef, fireDef, waterDef, mentalDef;
    [SerializeField]private Sprite blunt, pierce, fire, water, mental;
    public float GetDefenseOfType(SkillBase.SkillType skillType){
        switch(skillType){
            case SkillBase.SkillType.blunt:
                return bluntDef;
            case SkillBase.SkillType.pierce:
                return pierceDef;
            case SkillBase.SkillType.fire:
                return fireDef;
            case SkillBase.SkillType.water:
                return waterDef;
            case SkillBase.SkillType.mental:
                return mentalDef;
            default:
                return 0;
        }
    }

    public Sprite GetDefenseIcon(SkillBase.SkillType skillType){
        switch(skillType){
            case SkillBase.SkillType.blunt:
                return blunt;
            case SkillBase.SkillType.pierce:
                return pierce;
            case SkillBase.SkillType.fire:
                return fire;
            case SkillBase.SkillType.water:
                return water;
            case SkillBase.SkillType.mental:
                return mental;
            default:
                return null;
        }
    }

    public float GetBluntDef(){
        return bluntDef;
    }
    public float GetPierceDef(){
        return pierceDef;
    }
    public float GetFireDef(){
        return fireDef;
    }
    public float GetWaterDef(){
        return waterDef;
    }
    public float GetMentalDef(){
        return mentalDef;
    }
}
