using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeSkillPanels : MonoBehaviour
{
    [SerializeField]private GameObject skillButton;
    [SerializeField]private GameObject skillButtonsContainer;

    private GameObject[] players;
    void Awake()
    {
        //Get all the players
        players = GameObject.FindGameObjectsWithTag("Player");

        //Create skill buttons for each skill of player
        CreateSkillPanels();
    }

    void CreateSkillPanels(){
        foreach(GameObject player in players){
            GameObject newSkillButtonContainer = Instantiate(skillButtonsContainer, Vector3.zero, Quaternion.identity);
            newSkillButtonContainer.transform.SetParent(transform, false);

            CreateSkillButtons(player, newSkillButtonContainer.transform);
            newSkillButtonContainer.SetActive(false);
        }
    }

    void CreateSkillButtons(GameObject player, Transform parent){
        foreach(SO_Skill skill in player.GetComponent<SkillHolder>().GetSkills()){
            GameObject newSkillButton = Instantiate(skillButton, Vector3.zero, Quaternion.identity);
            newSkillButton.transform.SetParent(parent, false);
            newSkillButton.GetComponent<ReferencePlayer>().SetPlayerReference(player);

            //DEFINING SKILL INFO UI
            newSkillButton.name = skill.skillName;
            SetSkillInfo skillInfo = newSkillButton.GetComponent<SetSkillInfo>();
            skillInfo.SetName(skill.skillName);
            skillInfo.SetDescription(skill.description);

            //Defining skill actions
            SkillBase skillData = newSkillButton.AddComponent(System.Type.GetType(skill.GetSkillScript())) as SkillBase;
            skillData.SetDmgAmt(skill.dmgAmt);
            skillData.SetMaxStatusEffects(skill.maxStatusEffectAmount);

            //Give skill reference to player so player can set active their respective skill buttons when clicked
            player.GetComponent<ReferenceSkill>().SetSkillReference(newSkillButton);

            //ALSO CREATE STATUS EFFECTS TO INFUSE LATER
            GameObject statusEffectsContainer = GetComponent<MakeStatusEffectsPanel>().CreateEffectsPanel(player);
            statusEffectsContainer.transform.SetParent(transform, false);
            statusEffectsContainer.GetComponent<ReferenceSkill>().SetSkillReference(newSkillButton);
            statusEffectsContainer.SetActive(false);
            //REFERENCE STATUS EFFECT CONTAINER TO TOGGLE
            newSkillButton.GetComponent<ReferenceStatusEffectContainer>().SetStatusEffectContainer(statusEffectsContainer);
        }
    }

}
