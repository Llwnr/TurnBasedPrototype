using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDisappear : MonoBehaviour
{
    [SerializeField]private float timer;
    private float currTimer;
    [SerializeField]private float scaleSpeed;
    private void Start() {
        currTimer = timer;
    }
    private void Update() {
        currTimer -= Time.deltaTime;
        //Become bigger for half the duration, then become smaller
        if(currTimer > timer/2f){
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }else{
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
        }

        if(currTimer<0){
            Destroy(gameObject);
        }
    }
}
