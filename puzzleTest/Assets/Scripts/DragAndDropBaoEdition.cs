using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropBaoEdition : MonoBehaviour {
    

    // factors for the ui size hover increase 
    // beep boop

    // this is so that editors can change the values easily withouit having to read
    // i hate reading 
    public float hoverScaleX = 1.0f;
    public float hoverScaleY = 1.0f;

    private  Vector3 hoverScaler;

    // this is for the thing to return to in size
    private Vector3 initialScale;

    //define mouse position stuff so that we can drag and drop
    private Vector2 lastMousePosition;
    public Vector2 currentMousePosition;
    public Vector2 diffMousePosition;

    // copy and pasting mel things now...
    public bool cursorControlled;
    public int state;

    public bool held;
    public bool justReleased;
    public bool inside;

    // multiplies only the z value to 0
    // because then its only x and y
    Vector3 takeOutZ(Vector3 thang) {
        return Vector3.Scale(thang, new Vector3(1, 1, 0));
    }

    
    // Start is called before the first frame update
    void Start() {

        // set the scale vectors here to avoid errors
        hoverScaler = new Vector3(hoverScaleX, hoverScaleY, 0.0f);

        // makes it so that the mouse updating doesnt freak out
        lastMousePosition = takeOutZ(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // set the scale here for idk
        initialScale = transform.localScale;

        // mel things.. (pleawse work)
        cursorControlled = false;
        state = 0;
    }

    

    // Update is called once per frame
    void Update() {

        // inside AND held...
        if (inside && held){
            if (cursorControlled){
                state = 2;
            }else{
                state = 1;
            }

        // NOT held
        }else if (!held){
            state = 0;
        }

        
        // constantly update the mouseDiff
        updateMousePosition();

    }


    // the purpisoe of jouse over is that the mouse hovering makes the object larger
    // thats it

    
    void OnMouseOver() {

        inside = true;
        Debug.Log("Ping!");
        transform.localScale = Vector3.Scale(initialScale, hoverScaler);

        //mel things pllease help me understand
        if (Input.GetMouseButton(0)) // if left button clicked
        {
            held = true;
            justReleased = false;
        }else if (held){
            justReleased = true;
            held = false;
        }
    }
    

    // make it reutrn to original size
    void OnMouseExit() {
        inside = false;
        transform.localScale = initialScale;
    }

    // gives the mouse diff
    void updateMousePosition() {

        currentMousePosition = takeOutZ(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        diffMousePosition = currentMousePosition - lastMousePosition;
        lastMousePosition = currentMousePosition;
    }


    // whebevr mouse drags this, bring it to it?
    void OnMouseDrag() {
        Debug.Log("Ping!");

        Vector3 addThis = diffMousePosition;

        transform.position += (addThis);

        //cycle the mouse positions 
        lastMousePosition = currentMousePosition;
    }
}
