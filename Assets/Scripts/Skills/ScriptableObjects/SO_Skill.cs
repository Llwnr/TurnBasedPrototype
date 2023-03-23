using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SO_Skill", menuName = "TurnBased1/SO_Skill", order = 0)]
public class SO_Skill : ScriptableObject {
    public string skillName;
    [TextArea(15,20)]
    public string description;
    public int dmgAmt;
    public int maxStatusEffectAmount;
    [SerializeField]private MonoScript actionScript;

    public string GetSkillScript(){
        return actionScript.name;
    }
}
