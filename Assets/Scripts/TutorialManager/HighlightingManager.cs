using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightingManager : MonoBehaviour
{
    [SerializeField]private GameObject highlighter;
    private bool deactivateHighlighter = false;

    private void OnEnable() {
        highlighter.SetActive(false);
    }

    private void OnMouseDown() {
        //Activate highligher if the player's skill is active
        highlighter.SetActive(true);
    }

    private void OnMouseExit() {
        deactivateHighlighter = true;
    }

    private void OnMouseEnter() {
        deactivateHighlighter = false;
    }

    private void Update() {
        //If player clicked somewhere else
        if(deactivateHighlighter && Input.GetMouseButton(0)){
            highlighter.SetActive(false);
        }
    }
}
