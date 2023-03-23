using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackFromInfusion : MonoBehaviour
{
    public void WhenClicked(){
        gameObject.SetActive(false);
        GetComponent<ReferenceSkill>().GetReferencedSkillButton().transform.parent.gameObject.SetActive(true);
    }
}
