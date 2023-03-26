using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private GameObject objToDisplay;
    private bool isHovering = false;
    [SerializeField]private float hoverDuration;
    private float resetHoverDuration;

    private bool deactivateOnClick = false;

    private void Awake() {
        resetHoverDuration = hoverDuration;
        SetObjectDisplay(false);
    }

    public void OnPointerEnter(PointerEventData pointerEventData){
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        isHovering = false;
        SetObjectDisplay(false);
        ResetDuration();
    }

    //For gameobjects, activate on click
    private void OnMouseDown() {
        ToggleDisplay();
    }
    private void OnMouseExit() {
        deactivateOnClick = true;
    }

    void ToggleDisplay(){
        if(!isHovering)
            isHovering = true;
        else{
            isHovering = false;
            SetObjectDisplay(false);
            ResetDuration();
        }
    }

    private void OnDisable() {
        isHovering = false;
        ResetDuration();
    }

    private void OnEnable() {
        if(hoverDuration>0){
            SetObjectDisplay(false);
        }
    }

    void ResetDuration(){
        hoverDuration = resetHoverDuration;
        isHovering = false;
        deactivateOnClick = false;
    }

    private void Update() {
        if(isHovering){
            hoverDuration -= Time.deltaTime;
        }
        if(hoverDuration <= 0){
            SetObjectDisplay(true);
        }
        //If mouse click anywhere, deactivate panel
        if(deactivateOnClick && Input.GetMouseButtonDown(0)){
            ToggleDisplay();
        }
    }

    void SetObjectDisplay(bool value){
        objToDisplay.SetActive(value);
    }

}
