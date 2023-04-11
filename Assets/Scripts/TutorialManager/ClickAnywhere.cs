using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAnywhere : MonoBehaviour
{
    [SerializeField]private bool hasBeenClicked = false;

    public bool HasBeenClicked(){
        return hasBeenClicked;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            WhenClicked();
        }
    }

    public void WhenClicked() {
        if(hasBeenClicked) return;
        //Let player go to next step of tutorial where they need to click on next area
        hasBeenClicked = true;
        transform.parent.GetComponent<ManageClickingOrder>().GoToNextArea();
    }
}
