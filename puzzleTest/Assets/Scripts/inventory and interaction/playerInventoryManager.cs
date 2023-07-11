using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventoryManager : MonoBehaviour {
    public int invCapacity = 2;

    //create empty string array
    //this is the array used to store the items in the player's inventory
    public string[] playerInventoryContents = new string[] {};

    

    //we will need the following things:
    //detect an interaction
    //then we will see the interactee's inventory
    //if our inventory is below max capacity
    //copy their inventory contents to player inventory contents
    //then update the gui
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
