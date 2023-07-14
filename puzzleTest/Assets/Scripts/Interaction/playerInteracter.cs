using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteracter : MonoBehaviour {
    public int invCapacity = 2;

    //create empty string array
    //this is the array used to store the items in the player's inventory
    public string[] playerInventoryContents = new string[] {};

    private GameObject target;

    //we will need the following things:
    //detect an interaction
    //then we will see the interactee's inventory
    //if our inventory is below max capacity
    //copy their inventory contents to player inventory contents
    //then update the gui
    

    //this function will use a circle collider to fetch the nearest collision
    //using a layer filter thing will let me pick out only things from 'interactable' layer
    //so picking out only interactables will be pretty easy
    //NOTE TO SELF: THE ABOVE TWO LINES HAVE NOT BEEN IMPLEMENTED
    public Collider2D detectInteractables() {
        Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);

        //use overlap circle
        //this position is the pos of the transform
        //second arg is the range
        Collider2D result = Physics2D.OverlapCircle(thisPosition, 50.0f);
        return result;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        //when user presses the interact key
        //not sure what its gonna be yet
        if (Input.GetKeyDown("f")) {

            //we get a sample
            //
            Collider2D sample = detectInteractables();


            if (sample != null) {
                target = sample.gameObject;
            }
        }
    }
}
