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

    public int state;
    private bool held;
    public bool justReleased;
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
        //transform.position = mousePosition;
        
        originalScale = transform.localScale;
    }

    void OnMouseEnter() {
        inside = true;
    }

    void OnMouseOver() {
        /* FIXME: add logic to stop this from running if a piece is already being dragged. */

        // UI: scale-up object
        transform.localScale = new Vector3(originalScale.x * hoverFactor.x,
            originalScale.y * hoverFactor.y, 
            originalScale.z * hoverFactor.z);
        
        if (Input.GetMouseButton(0)) // if left button clicked
        {
            held = true;
            justReleased = false;
        }else if (held){
            justReleased = true;
            held = false;
        }
    }

    void OnMouseExit() {
        inside = false;
        // would like item to go back to regular size
        transform.localScale = originalScale;
    }

    void Update() {
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
