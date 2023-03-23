using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceStatusEffectContainer : MonoBehaviour
{
    [SerializeField]private GameObject statusEffectContainer;
    public void SetStatusEffectContainer(GameObject statusEffectContainer){
        this.statusEffectContainer = statusEffectContainer;
    }

    public GameObject GetStatusEffectContainer(){
        return statusEffectContainer;
    }
}
