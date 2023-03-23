using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencePlayer : MonoBehaviour
{
    private GameObject player;
    public void SetPlayerReference(GameObject player){
        this.player = player;
    }

    public GameObject GetReferencedPlayer(){
        return player;
    }
}
