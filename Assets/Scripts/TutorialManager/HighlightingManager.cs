using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightingManager : MonoBehaviour
{
    [SerializeField]private GameObject highlighter;

    private void OnEnable() {
        highlighter.SetActive(false);
    }

    private void Update() {
        //Activate highligher if the player's skill is active
        highlighter.SetActive(GetComponent<ReferenceSkill>().GetReferencedSkillButton().transform.parent.gameObject.activeSelf);
    }
}
