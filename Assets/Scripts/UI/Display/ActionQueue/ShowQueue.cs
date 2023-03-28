using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ShowQueue : MonoBehaviour
{
    private AttackManager attackManager;
    [SerializeField]private GameObject displayActionsQueue;
    private void Awake() {
        attackManager = GetComponent<AttackManager>();
    }

    private void Update() {
        DisplayActionQueue();
    }


    //Show the skill queue that will execute one by one in their respective order in that turn
    void DisplayActionQueue(){
        SkillBase[] skillQueue = attackManager.GetSkillQueue().ToArray();
        string queueInString = "";
        for(int i=0; i<skillQueue.Length; i++){
            queueInString += skillQueue[i].GetAttacker().name + " -> " + skillQueue[i].GetType().Name + " -> " + skillQueue[i].GetTarget().name + "\n";
            if(attackManager.IsActionExecuting() && i==0){
                //Decorate to show that the first action is being executed
                queueInString = "<color=#FFFF00>" + "Executing: " + queueInString + "</color>";
            }
        }

        displayActionsQueue.GetComponent<TextMeshProUGUI>().text = queueInString;
    }
}
