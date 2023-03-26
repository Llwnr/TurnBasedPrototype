using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnClick : MonoBehaviour
{
    private void Awake() {
        gameObject.SetActive(false);
    }
    public void Toggle(){
        if(gameObject.activeSelf){
            gameObject.SetActive(false);
        }else{
            gameObject.SetActive(true);
        }
    }
}
