using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDefense : MonoBehaviour
{
    [SerializeField]private DefenseManager defenseManager;
    private void Awake() {
        Debug.Log(defenseManager.GetBluntDef() + " " + defenseManager.GetFireDef());
    }
}
