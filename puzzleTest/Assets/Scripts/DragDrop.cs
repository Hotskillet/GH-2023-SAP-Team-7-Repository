using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FSM
0 = no follow
1 = follow
*/

public class DragDrop : MonoBehaviour
{
    // UI VALUES
    public Vector3 originalScale;
    public Vector3 hoverFactor;

    private Vector3 mousePosition;
    private Camera cam;

    private int state;
    private bool held;
    private bool justReleased;
    private bool inside;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        held = false;
        justReleased = false;
        cam = Camera.main;
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        transform.position = mousePosition;
        
        originalScale = transform.localScale;
    }

    void OnMouseEnter() {
        inside = true;
    }

    void OnMouseOver() {
        // UI: scale-up object
        transform.localScale = new Vector3(originalScale.x * hoverFactor.x,
            originalScale.y * hoverFactor.y, 
            originalScale.z * hoverFactor.z);

        if (Input.GetMouseButton(0)) // if left button clicked
        {
            held = true;
            justReleased = false; //FIXME: remove when raise is added
        }else if (held){
            justReleased = true;
            held = false;
            //FIXME: add snap raise here, then set justRealeased to false
        }
    }

    void OnMouseExit() {
        inside = false;
        // would like item to go back to regular size
        transform.localScale = originalScale;
    }

    void Update() {
        if (justReleased){
            Debug.Log("Just released.");
        }

        /* 
        Update State
            Allows object to follow mouse even if mouse moves outside of it (as long as left-click
            is still being held down)
        */
        if (inside && held){
            state = 1;
        }else if (!held){
            state = 0;
        }

        switch (state){
            case 0:
                break;
            case 1:
                // keep track of where mouse is
                mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0.0f;
                // make object follow mouse
                transform.position = mousePosition;
                break;
            default:
                break;
        }
    }
}
