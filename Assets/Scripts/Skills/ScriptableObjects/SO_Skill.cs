using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Skill", menuName = "TurnBased1/SO_Skill", order = 0)]
public class SO_Skill : ScriptableObject {
    public string skillName;
    [TextArea(15,20)]
    public string description;
    public int dmgAmt;
    public int maxStatusEffectAmount;
    public SkillBase.SkillType skillType;
    [SerializeField]private TextAsset actionScript;

    public string GetSkillScript(){
        return actionScript.name;
    }
}
