using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    [SerializeField]private List<SO_Skill> mySkills = new List<SO_Skill>();

    public List<SO_Skill> GetSkills(){
        return mySkills;
    }
}
