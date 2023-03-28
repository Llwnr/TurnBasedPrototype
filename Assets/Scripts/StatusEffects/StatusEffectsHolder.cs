using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusEffectsHolder : MonoBehaviour
{
    [SerializeField]private List<SO_StatusEffect> myStatusEffects = new List<SO_StatusEffect>();
    //To show status effects that were previously deleted when you infused and now you need to show those effects when deinfused
    [SerializeField]private SO_StatusEffect[] backupEffects;

    public List<SO_StatusEffect> GetStatusEffects(){
        return myStatusEffects;
    } 

    public void InfuseEffect(SO_StatusEffect statusEffect){
        myStatusEffects.Add(statusEffect);
        backupEffects = myStatusEffects.ToArray();
        if(GetComponent<DisplayInfusedEffectsOnSkill>()){
            GetComponent<DisplayInfusedEffectsOnSkill>().CreateEffectIcon(statusEffect);
        }
    }

    private void Update() {
        SkillBase mySkill = GetComponent<SkillBase>();
        if(mySkill == null) return;

        while(myStatusEffects.Count > mySkill.GetMaxStatusEffects()){
            myStatusEffects.RemoveAt(myStatusEffects.Count-1);
        }
        BackupEffects(mySkill);
    }

    void BackupEffects(SkillBase mySkill){
        //As long as status effects shown is less than the skills max capacity and there are effects in backup, show it
        if(backupEffects == null) return;
        while(myStatusEffects.Count < mySkill.GetMaxStatusEffects() && backupEffects.Length > myStatusEffects.Count){
            int index = myStatusEffects.Count;
            myStatusEffects.Add(backupEffects[index]);
            if(GetComponent<DisplayInfusedEffectsOnSkill>()){
                GetComponent<DisplayInfusedEffectsOnSkill>().CreateEffectIcon(backupEffects[index]);
            }
        }
    }

    public void ClearAll(){
        Debug.Log("cleared");
        myStatusEffects.Clear();
        backupEffects = null;
    }
}
