using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleStatusEffectsIcon : MonoBehaviour
{
    public void ToggleStatusEffects(){
        GameObject skillButtonContainer = transform.parent.transform.parent.gameObject;
        skillButtonContainer.SetActive(false);

        GameObject statusEffectsContainer = transform.parent.GetComponent<ReferenceStatusEffectContainer>().GetStatusEffectContainer();
        statusEffectsContainer.SetActive(true);
    }
}
