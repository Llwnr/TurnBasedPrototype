using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField]private float dimDuration;

    private void OnEnable() {
        StartHighlight();
    }

    private void OnDisable() {
        StopHighlight();
    }

    public void StartHighlight(){
        StartCoroutine(HighlightSprite(dimDuration));
    }

    public void StopHighlight(){
        StopAllCoroutines();
    }



    IEnumerator HighlightSprite(float maxDuration){
        float duration = maxDuration;
        int incrOrDecr = 1;
        Color opacityChanger = GetComponent<SpriteRenderer>().color;
        while(true){
            duration -= Time.deltaTime * incrOrDecr;
            opacityChanger.a = duration/maxDuration;
            GetComponent<SpriteRenderer>().color = opacityChanger;
            if(duration < 0 || duration > maxDuration){
                incrOrDecr *= -1;
            }

            yield return null;
        }
    }
}
