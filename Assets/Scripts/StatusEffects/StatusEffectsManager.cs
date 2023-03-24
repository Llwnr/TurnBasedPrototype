using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsManager : O_StatusEffect
{
    //ONLY ONE CLASS PER GAMEOBJECT
    private void OnEnable() {
        if(GetComponents<O_StatusEffect>().Length > 1) Debug.LogError("More than one status effect observer");
    }
    //The one who attacks
    [SerializeField]private GameObject attacker;
    public void SetAttacker(GameObject attacker){
        this.attacker = attacker;
    }
    public GameObject GetAttacker(){
        return attacker;
    }
}
