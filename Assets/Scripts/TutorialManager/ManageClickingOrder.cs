using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageClickingOrder : MonoBehaviour
{
    [SerializeField]private List<GameObject> clickAreas = new List<GameObject>();
    private int index = 0;
    // Start is called before the first frame update
    void Awake()
    {
        clickAreas.Clear();
        for(int i=0; i<transform.childCount; i++){
            //Add all the tutorial highlight cues
            clickAreas.Add(transform.GetChild(i).gameObject);
        }
        //Only activate one clickable area at a time
        for(int i=0; i<clickAreas.Count; i++){
            if(i == index) continue;

            clickAreas[i].SetActive(false);
        }
    }

    public void GoToNextArea(){
        Debug.Log("Move to next area");
        clickAreas[index].SetActive(false);
        index++;
        if(index >= clickAreas.Count) return;
        //Activate next click area
        clickAreas[index].SetActive(true);
    }
}
