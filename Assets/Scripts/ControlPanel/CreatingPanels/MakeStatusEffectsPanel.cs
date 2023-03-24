using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakeStatusEffectsPanel : MonoBehaviour
{
    [SerializeField]private GameObject effectsButton;
    [SerializeField]private GameObject effectsContainer;

    private List<SO_StatusEffect> effectsCreated = new List<SO_StatusEffect>();

    private GameObject[] players;
    void Awake()
    {
        //Get all the players
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    //MakeSkillPanels will create status effects panel and add it as child of skill button. For infusing
    public GameObject CreateEffectsPanel(GameObject statusEffectsofPlayer){
        GameObject newStatusEffectsContainer = Instantiate(effectsContainer, Vector3.zero, Quaternion.identity);
        newStatusEffectsContainer.transform.SetParent(transform, false);

        CreateEffectsButton(statusEffectsofPlayer, newStatusEffectsContainer.transform);
        return newStatusEffectsContainer;
    }

    void CreateEffectsButton(GameObject player, Transform parent){
        effectsCreated.Clear();
        //Create 5 random status effects
        while(effectsCreated.Count < 5){
            SO_StatusEffect[] statusEffects = player.GetComponent<StatusEffectsHolder>().GetStatusEffects().ToArray();
            SO_StatusEffect statusEffect = statusEffects[Random.Range(0, player.GetComponent<StatusEffectsHolder>().GetStatusEffects().Count)];
            //Check if the effect is already created, in that case make a different one
            if(effectsCreated.Contains(statusEffect)) continue;
            else{
                effectsCreated.Add(statusEffect);
            }
            GameObject newEffectsButton = Instantiate(effectsButton, Vector3.zero, Quaternion.identity);
            newEffectsButton.transform.SetParent(parent, false);
            newEffectsButton.GetComponent<InfuseToSkill>().SetStatusEffect(statusEffect);
            //Setting display information
            newEffectsButton.GetComponent<Image>().sprite = statusEffect.effectIcon;
            newEffectsButton.GetComponent<SetEffectInfo>().SetEffectCount(statusEffect.effectCount.ToString());
            newEffectsButton.GetComponent<SetEffectInfo>().SetDescription(statusEffect.effectName, statusEffect.description);
        }
    }
}
