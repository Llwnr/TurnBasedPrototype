using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    //Store the actions to perform
    [SerializeField]private List<SkillBase> skillQueue = new List<SkillBase>();
    
    public void AddToQueue(SkillBase skill){
        skillQueue.Add(skill);
    }

    public void RemoveFromQueue(SkillBase skill){
        skillQueue.Remove(skill);
    }

    
    public void StartExecuteActions(){
        StartCoroutine(ExecuteActions());
    }

    public IEnumerator ExecuteActions(){
        foreach(SkillBase skill in skillQueue){
            skill.ExecuteSkillAction();
            Debug.Log("Attacker: " + skill.GetAttacker() + " Target: " + skill.GetTarget());
            Debug.Log("Work left here");
            //Make an object that will hit the target. When it hits start its animation, once animation ends execute skill action
            yield return new WaitForSeconds(1f);
        }
        yield return StartCoroutine(ExecuteEnemyActions());
        yield return new WaitForSeconds(1f);
        InvokeEndOfTurnEffects();

        

        skillQueue.Clear();
    }

    //Invoke OnTurnEnd status effects for each game objects
    void InvokeEndOfTurnEffects(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies){
            StatusEffectsManager targetManager = enemy.GetComponent<StatusEffectsManager>();
            targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnTurnEnd);
        }
    }

    IEnumerator ExecuteEnemyActions(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies){
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
                StatusEffectsManager targetManager = enemy.GetComponent<StatusEffectsManager>();
                StatusEffectsManager playerManager = player.GetComponent<StatusEffectsManager>();
                targetManager.SetSkillDmgAmt(10);
                float enemyDmg = targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnAttack);
                playerManager.SetSkillDmgAmt(enemyDmg);
                playerManager.SetAttacker(enemy);
                float finalDmg = playerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnHit);
                playerManager.SetSkillDmgAmt(finalDmg);
                float trueDmg = playerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnFinalDmg);
                player.GetComponent<HealthManager>().DamagePlayerBy(trueDmg);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
