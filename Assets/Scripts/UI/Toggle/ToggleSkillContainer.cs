using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillContainer : MonoBehaviour
{
    private bool selectingTarget = false;//Disable toggling if player is selecting target
    private void ToggleSkills() {
        GameObject controlPanel = GameObject.FindWithTag("ControlPanel");
        //If while targetting, clicked on player, then just set target. Don't toggle anything
        if(selectingTarget) return;
        //Deactivate all other player's skill buttons
        for(int i=0; i<controlPanel.transform.childCount; i++){
            controlPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        GetComponent<ReferenceSkill>().GetReferencedSkillButton().transform.parent.gameObject.SetActive(true);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            for(int i=0; i<hits.Length; i++){
                if(hits[i].collider != null && hits[i].transform.tag == "Player"){
                    ToggleSkills();
                }
            }
        }
    }

    public void SetTargetSelection(bool value){
        selectingTarget = value;
    }
}
