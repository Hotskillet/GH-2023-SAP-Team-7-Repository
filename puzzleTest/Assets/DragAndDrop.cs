using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour{
    
    //define mouse position stuff so that we can drag and drop
    private Vector2 lastMousePosition;
    private Vector2 currentMousePosition;
    private Vector2 diffMousePosition;


    // Start is called before the first frame update
    void Start() {
        //start off last mouse position so that it isnt empty
        lastMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    

    void OnMouseDrag() {
        print("YEAH");
        
        /*
        currentMousePosition = Input.mousePosition;
        diffMousePosition = currentMousePosition - lastMousePosition;
    
        
        Vector3 addThis = new Vector3(diffMousePosition.x, diffMousePosition.y, 0);
        print(addThis);
        transform.position += addThis;
        
        lastMousePosition = currentMousePosition;
        */
    }
}
