using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickedOnMe : MonoBehaviour
{
    [SerializeField]private bool hasBeenClicked = false;

    public bool HasBeenClicked(){
        return hasBeenClicked;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            CheckClickOnMe();
        }
    }

    void CheckClickOnMe(){
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        for(int i=0; i<hits.Length; i++){
            if(hits[i].collider != null && hits[i].transform.tag == "Tutorial"){
                WhenClicked();
            }
        }
    }

    public void WhenClicked() {
        if(hasBeenClicked) return;
        //Let player go to next step of tutorial where they need to click on next area
        hasBeenClicked = true;
        transform.parent.GetComponent<ManageClickingOrder>().GoToNextArea();
    }
}
