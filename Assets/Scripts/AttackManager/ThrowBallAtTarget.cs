using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallAtTarget : MonoBehaviour
{
    private Transform origin, target;
    [SerializeField]private GameObject ballToThrow;
    private bool hasCollidedWithTarget = false;
    //Reset management
    private void OnEnable() {
        ballToThrow.SetActive(false);
    }

    public GameObject GetBall(){
        return ballToThrow;
    }

    //Set the ball's destination
    public void SetTargets(GameObject attacker, GameObject target){
        if(!gameObject.activeSelf) return;
        hasCollidedWithTarget = false;
        ballToThrow.SetActive(true);
        ballToThrow.transform.position = attacker.transform.position;
        origin = attacker.transform;
        this.target = target.transform;

        StartCoroutine(ThrowBall());
    }

    IEnumerator ThrowBall(){
        StartCoroutine(SetBallAsHit());
        NotifyBallCollision ballCollision = ballToThrow.GetComponent<NotifyBallCollision>();
        ballCollision.SetTarget(target);
        //Move ball closer to target until it has collided with it
        while(!hasCollidedWithTarget){
            if(ballCollision.HasBallCollided()){
                ballToThrow.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                hasCollidedWithTarget = true;
                yield break;
            }
            Vector2 dir = target.position - transform.position;
            ballToThrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y).normalized*1000f);
            yield return null;
        }
    }

    IEnumerator SetBallAsHit(){
        yield return new WaitForSeconds(5);
        hasCollidedWithTarget = true;
    }

    public bool HasBallCollided(){
        return hasCollidedWithTarget;
    }

    public void SetBallInactive(){
        ballToThrow.SetActive(false);
    }
}
