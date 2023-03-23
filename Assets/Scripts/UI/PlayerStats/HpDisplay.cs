using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour
{
    private float hp, maxHp;

    private void Start() {
        maxHp = transform.parent.transform.parent.GetComponent<HealthManager>().GetMaxHealth();
    }
    private void Update() {
        hp = transform.parent.transform.parent.GetComponent<HealthManager>().GetCurrentHealth();
        transform.GetChild(0).GetComponent<Image>().fillAmount = hp/maxHp;
    }
}
