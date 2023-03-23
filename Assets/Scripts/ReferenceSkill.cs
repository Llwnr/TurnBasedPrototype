using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceSkill : MonoBehaviour
{
    [SerializeField]private GameObject skillBtn;
    public void SetSkillReference(GameObject skillBtn){
        this.skillBtn = skillBtn;
    }

    public GameObject GetReferencedSkillButton(){
        return skillBtn;
    }
}
