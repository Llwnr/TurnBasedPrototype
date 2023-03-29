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
        //Only activate one clickable area at a time
        for(int i=0; i<clickAreas.Count; i++){
            if(i == index) continue;

            clickAreas[i].SetActive(false);
        }
    }

    public void GoToNextArea(){
        Debug.Log("Move to next area");
        index++;
        if(index >= clickAreas.Count) index = clickAreas.Count-1;
        //Activate next click area
        clickAreas[index].SetActive(true);
    }
}
