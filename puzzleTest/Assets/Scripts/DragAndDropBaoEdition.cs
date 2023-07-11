using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
    




    //define mouse position stuff so that we can drag and drop
    private Vector2 lastMousePosition;
    private Vector2 currentMousePosition;
    private Vector2 diffMousePosition;


    //we're using another gameObject to track the mouse position 
    //therefore making it really simple to track mouse delta
    public Transform trackerTarget;

    // Start is called before the first frame update
    void Start() {
        //start off last mouse position so that it isnt empty
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update() {

    }    

    //whebevr mouse drags this, bring it to it?
    void OnMouseDrag() {

        /*
        commenting the following because testing out new tracker

        //get the mouse pos as coords
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //get the difference
        diffMousePosition = currentMousePosition - lastMousePosition;
        

        //add the difference (while cancelling out the z)
        Vector3 addThis = new Vector3(diffMousePosition.x, diffMousePosition.y, 0.0f);

        */


        //basically, i set the mouse delta to be the tracker targets position
        //so now addThis is just the mouse delta
        //probably less responsive in terms of mouse stuff but definitely more painfree
        Vector3 addThis = trackerTarget.position;

        transform.position += (addThis);

        //cycle the mouse positions 
        lastMousePosition = currentMousePosition;
    }
}
