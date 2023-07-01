using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapperCrude : MonoBehaviour
{
    //when the mothership is dropped
    //the goober checks for others
    //if it detects any, it runs a coordinate check
    //coordinates as in the coord id's of the other motherships

    private bool activeness = false;

    private Collider2D[] listOfCollisions;

    //when the mouse goes down then its active
    void OnMouseDown() {
        activeness = true;
    }
    
    void OnMouseUp() {

        //if the thing is active then we snap
        //might need to check later about the activeness check thing being redundant
        //because onMouseUp might be all we need 
        //i think it is
        //better safe than sorry

        if (activeness) {
            activeness = false;
            //new int OverlapCollider(ContactFilter2D NoFilter(), listOfCollisions);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
