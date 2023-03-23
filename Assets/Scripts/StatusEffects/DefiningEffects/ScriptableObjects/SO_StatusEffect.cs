using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SO_StatusEffect", menuName = "TurnBased1/SO_StatusEffect", order = 0)]
public class SO_StatusEffect : ScriptableObject {
    public string effectName;
    [TextArea(15,20)]
    public string description;
    public int effectCount;
    public Sprite effectIcon;
    public bool canStack;
    public StatusEffectBase.ActivationCondition activationCondition;
    [SerializeField]private MonoScript effectScript;

    public string GetEffectScript(){
        return effectScript.name;
    }
}

