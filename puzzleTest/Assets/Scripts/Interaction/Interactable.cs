using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //FIXME
    public override void interact(){
        /* Steps:
            1. Do nothing, this is only for items that can be picked up
        */
        Debug.Log("no.");
        return;
    }

    //FIXME
    public override void use()
    {
        /* Steps:
            1. For all items in Player's inventory, check if in this object's list
            3. do some interaction
        */
        Debug.Log("use() is a WIP.");
        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
