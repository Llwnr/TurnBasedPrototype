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
            Debug.Log(skill.GetType());
            //Wait for attack to hit, then execute skill
            yield return StartCoroutine(WaitTillBallCollides(skill));
            skill.ExecuteSkillAction();
        }
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(ExecuteEnemyActions());
        InvokeEndOfTurnEffects();
        skillQueue.Clear();
    }

    //Check if the ball has hit enemy. In that case, execute its skill action and inflict status effects
    IEnumerator WaitTillBallCollides(SkillBase skill){
        ThrowBallAtTarget ballThrow = skill.GetAttacker().GetComponent<ThrowBallAtTarget>();
        ballThrow.SetTargets(skill.GetAttacker(), skill.GetTarget());
        //Show the status effects inflicted on the skill button onto the ball
        DisplayInfusedEffectsOnSkill skillInfusedEffects = ballThrow.GetBall().GetComponent<DisplayInfusedEffectsOnSkill>();
        foreach(SO_StatusEffect statusEffect in skill.GetComponent<StatusEffectsHolder>().GetStatusEffects()){
            skillInfusedEffects.CreateEffectIcon(statusEffect);
        }
        yield return new WaitUntil(() => ballThrow.HasBallCollided());
        //Wait till player clicks
        while(!Input.GetMouseButton(0)){
            yield return null;
        }
        ballThrow.SetBallInactive();
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
                playerManager.SetAttacker(enemy);
                targetManager.SetSkillDmgAmt(10);
                float enemyDmg = targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnAttack);
                playerManager.SetSkillDmgAmt(enemyDmg);
                float finalDmg = playerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnHit);
                playerManager.SetSkillDmgAmt(finalDmg);
                float trueDmg = playerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnFinalDmg);
                player.GetComponent<HealthManager>().DamagePlayerBy(trueDmg);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
