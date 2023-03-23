using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyBallCollision : MonoBehaviour
{
    private Transform target;
    public void SetTarget(Transform target){
        this.target = target;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(target != null && other.transform.gameObject == target.gameObject){
            Debug.Log("Collided");
        }
    }
}
