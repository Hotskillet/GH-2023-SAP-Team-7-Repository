using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseUpdater : MonoBehaviour
{

    //define some mouse psotioon sutff
    public Vector3 mouseDiff;
    private Vector3 currentMousePosition;
    private Vector3 lastMousePosition;
    
    //multiplies only the z value to 0
    //because then its only x and y
    Vector3 takeOutZ(Vector3 thang) {
        return Vector3.Scale(thang, new Vector3(1, 1, 0));
    }

    // Start is called before the first frame update
    void Start() {

        lastMousePosition = takeOutZ(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    // Update is called once per frame
    void Update() {

        currentMousePosition = takeOutZ(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseDiff = currentMousePosition - lastMousePosition;


        //crude way to track the difference
        transform.position = mouseDiff;

        lastMousePosition = currentMousePosition;

        
    }
}
