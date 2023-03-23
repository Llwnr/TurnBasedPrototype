using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyBallCollision : MonoBehaviour
{
    private Transform target;
    [SerializeField]private bool hasCollided;
    private void OnDisable() {
        hasCollided = false;
    }
    public void SetTarget(Transform target){
        this.target = target;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(target != null && other.transform.gameObject == target.gameObject){
            hasCollided = true;
        }
    }
    public bool HasBallCollided(){
        return hasCollided;
    }
}
