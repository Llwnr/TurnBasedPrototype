using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedOnMe : MonoBehaviour
{
    private bool hasBeenClicked = false;

    public bool HasBeenClicked(){
        return hasBeenClicked;
    }

    private void OnMouseDown() {
        WhenClicked();
    }
    public void WhenClicked() {
        if(hasBeenClicked) return;
        //Let player go to next step of tutorial where they need to click on next area
        hasBeenClicked = true;
        transform.parent.GetComponent<ManageClickingOrder>().GoToNextArea();
    }
}
