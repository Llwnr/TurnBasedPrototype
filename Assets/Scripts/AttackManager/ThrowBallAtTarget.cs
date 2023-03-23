using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallAtTarget : MonoBehaviour
{
    private Transform origin, target;
    [SerializeField]private GameObject ballToThrow;
    private bool hasCollidedWithTarget = false;
    public void SetTargets(GameObject attacker, GameObject target){
        hasCollidedWithTarget = false;
        ballToThrow.transform.position = attacker.transform.position;
        origin = attacker.transform;
        this.target = target.transform;

        StartCoroutine(ThrowBall());
    }

    IEnumerator ThrowBall(){
        //Move ball closer to target until it has collided with it
        while(!hasCollidedWithTarget){
            Vector2 dir = target.position - transform.position;
            ballToThrow.GetComponent<NotifyBallCollision>().SetTarget(target);
            ballToThrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x, dir.y).normalized*1000f);
            yield return null;
        }
    }
}
