using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AttackManager : MonoBehaviour
{
    //Store the actions to perform
    [SerializeField]private List<SkillBase> skillQueue = new List<SkillBase>();
    [SerializeField]private bool actionsExecuting = false;

    [SerializeField]private TextMeshProUGUI attackBtn;
    
    public void AddToQueue(SkillBase skill){
        skillQueue.Add(skill);
    }

    public void RemoveFromQueue(SkillBase skill){
        skillQueue.Remove(skill);
    }

    public List<SkillBase> GetSkillQueue(){
        return skillQueue;
    }

    public bool IsActionExecuting(){
        return actionsExecuting;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    
    public void StartExecuteActions(){
        //Only execute actions once all actions are finished
        if(!actionsExecuting){
            actionsExecuting = true;
            attackBtn.text = "WAIT FOR TURN END";
        }else{
            return;
        }
        StartCoroutine(ExecuteActions());
    }

    public IEnumerator ExecuteActions(){
        for(int i=0; i<skillQueue.Count; i++){
            SkillBase skill = skillQueue[0];
            //Don't activate skills of dead ones or if the target is dead
            if(!skill.gameObject.activeSelf || !skill.GetTarget().activeSelf) continue;
            Debug.Log(skill.GetType());
            //Wait for attack to hit, then execute skill
            yield return StartCoroutine(WaitTillBallCollides(skill));
            skill.ExecuteSkillAction();
            skillQueue.RemoveAt(i);
            i--;
        }
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(ExecuteEnemyActions());
        yield return new WaitForSeconds(1);
        InvokeEndOfTurnEffects();

        skillQueue.Clear();
        actionsExecuting = false;
        attackBtn.text = "Attack";
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players){
            StatusEffectsManager targetManager = player.GetComponent<StatusEffectsManager>();
            targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnTurnEnd);
        }
    }

    //FOR ENEMY ATTACKS
    IEnumerator ExecuteEnemyActions(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies){
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
                //Throw ball as attack
                yield return StartCoroutine(WaitTillEnemyBallHits(enemy,player));

                StatusEffectsManager targetManager = enemy.GetComponent<StatusEffectsManager>();
                StatusEffectsManager playerManager = player.GetComponent<StatusEffectsManager>();
                playerManager.SetAttacker(enemy);
                targetManager.SetSkillDmgAmt(16);
                float enemyDmg = targetManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnAttack);
                playerManager.SetSkillDmgAmt(enemyDmg);
                float finalDmg = playerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnHit);
                playerManager.SetSkillDmgAmt(finalDmg);
                float trueDmg = playerManager.GetDmgAfterStatusEffects(StatusEffectBase.ActivationCondition.OnFinalDmg);
                CalculateDefenseAndDamage(player, trueDmg, SkillBase.SkillType.fire);
            }
        }
    }

    //Calculate damage after defense and then deal damage
    public static void CalculateDefenseAndDamage(GameObject target, float dmgAmt, SkillBase.SkillType damageType){
        float def = target.GetComponent<DefenseManager>().GetDefenseOfType(damageType);
        float dmgToDeal = dmgAmt/def;
        target.GetComponent<HealthManager>().DamagePlayerBy(dmgToDeal);
    }

    IEnumerator WaitTillEnemyBallHits(GameObject enemy, GameObject player){
        ThrowBallAtTarget ballThrow = enemy.GetComponent<ThrowBallAtTarget>();
        ballThrow.SetTargets(enemy, player);

        yield return new WaitUntil(() => ballThrow.HasBallCollided());
        //Wait till player clicks
        while(!Input.GetMouseButton(0)){
            yield return null;
        }
        ballThrow.SetBallInactive();
    }
}
