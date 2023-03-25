using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectTarget : MonoBehaviour
{
    //To know if user is selecting target. If selecting target then don't display other players skill buttons when user clicks on them to set target
    //SetSelectionActive();
    private LineRenderer line;
    private GameObject target;
    private void Awake() {
        line = GetComponent<LineRenderer>();
    }

    //Make a line to show targeting
    public void WhenClicked(){
        ResetTargets();
        line.positionCount = 2;
        line.SetPosition(0, transform.position - new Vector3(0,0,50));
        //To know that user is selecting a target
        StartCoroutine(SetSelectionActive(true));
        StartCoroutine(MakeLine());
    }

    IEnumerator MakeLine(){
        while(!Input.GetMouseButtonDown(0)){
            line.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,50));
            
            //Cancel selection on left click
            if(Input.GetMouseButtonDown(1)){
                ResetTargets();
                yield break;
            }
            yield return null;
        }
        GetTargetByRaycast();
        StartCoroutine(SetSelectionActive(false));
        yield break;
    }

    void ResetTargets(){
        StartCoroutine(SetSelectionActive(false));
        RemoveLine();
        GetComponent<SkillBase>().ResetTargetOnCancellation();
    }

    //Make player changing to select their skills available
    IEnumerator SetSelectionActive(bool value){
        yield return null;
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
            player.GetComponent<ToggleSkillContainer>().SetTargetSelection(value);
        }
    }

    public void RemoveLine(){
        line.positionCount = 0;
    }

    void GetTargetByRaycast(){
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
        for(int i=0; i<hits.Length; i++){
            if(hits[i].collider != null && (hits[i].transform.tag == "Enemy" || hits[i].transform.tag == "Player")){
                line.SetPosition(1, hits[i].transform.position);
                target = hits[i].transform.gameObject;

                //Setting target
                GetComponent<SkillBase>().SetTargets(GetComponent<ReferencePlayer>().GetReferencedPlayer(), target);
            }
        }
    }

    public GameObject GetTarget(){
        return target;
    }
}
