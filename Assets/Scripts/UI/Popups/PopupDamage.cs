using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupDamage : MonoBehaviour, IReceivedDamage
{
    private void OnEnable() {
        GetComponent<HealthManager>().AddHpObserver(this);
    }
    private void OnDisable() {
        GetComponent<HealthManager>().RemoveObserver(this);
    }
    [SerializeField]private GameObject popupText;
    public void OnDmgReceived(float dmgAmt){
        Vector3 offset;
        GetDisplayDirection(out offset);
        //Check whether to show popup on left side or right side of gameobject
        GameObject newPopupText = Instantiate(popupText, Vector3.zero, Quaternion.identity);
        newPopupText.GetComponent<TextMeshProUGUI>().text = dmgAmt.ToString("F2");
        newPopupText.transform.SetParent(transform.GetChild(0).transform, false);
        newPopupText.transform.position = Camera.main.WorldToScreenPoint(transform.position+offset);
    }

    void GetDisplayDirection(out Vector3 offset){
        StatusEffectsManager statusEffectsManager = GetComponent<StatusEffectsManager>();
        //For self damage cases
        Transform attacker = null;
        if(statusEffectsManager.GetAttacker() == null){
            attacker = transform;
        }else{
            attacker = statusEffectsManager.GetAttacker().transform;
        }
        if((attacker.position.x - transform.position.x) > 0){
            offset = new Vector3(-1,0,0);
        }else{
            offset = new Vector3(1,0,0);
        }
    }
}
