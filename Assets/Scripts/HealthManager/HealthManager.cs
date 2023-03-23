using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{   
    private List<IReceivedDamage> observers = new List<IReceivedDamage>();
    public void AddHpObserver(IReceivedDamage observer){
        observers.Add(observer);
    }
    public void RemoveObserver(IReceivedDamage observer){
        observers.Remove(observer);
    }
    void NotifyObservers(float dmgAmt){
        foreach(IReceivedDamage observer in observers){
            observer.OnDmgReceived(dmgAmt);
        }
    }
    //MANAGE OBSERVERS
    [SerializeField]private float health;
    private float maxHealth;
    // Start is called before the first frame update
    private void Awake() {
        maxHealth = health;
        if(GetComponents<HealthManager>().Length > 1){
            Debug.LogError("More than one health manager in a gameobject");
        }
    }

    public void DamagePlayerBy(float dmgAmt){
        health -= dmgAmt;
        NotifyObservers(dmgAmt);
    }

    public float GetMaxHealth(){
        return maxHealth;
    }
    public float GetCurrentHealth(){
        return health;
    }
}
