using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayOnRightClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private GameObject objToDisplay;
    private bool isHovering = false;

    private void Awake() {
        SetObjectDisplay(false);
    }

    private void OnEnable() {
        SetObjectDisplay(false);
    }

    public void OnPointerEnter(PointerEventData pointerEventData){
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        isHovering = false;
        SetObjectDisplay(false);
    }

    private void OnDisable() {
        isHovering = false;
    }

    private void Update() {
        if(isHovering && Input.GetMouseButton(1)){
            SetObjectDisplay(true);
        }
    }

    void SetObjectDisplay(bool value){
        objToDisplay.SetActive(value);
    }

}
