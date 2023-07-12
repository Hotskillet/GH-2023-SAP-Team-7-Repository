using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Item
{
    // Start is called before the first frame update
    void Start()
    {
    }

    //FIXME
    public override void interact(){
        // Step 1: Add name of item to inventory
        Inventory.Instance.AddItem(gameObject.name);
        //FIXME Step 2: Tell UI to add sprite to inventory bar
        Debug.Log(gameObject.name + " has been picked up.");
        // Step 3: Delete object from world
        Destroy(gameObject, 0.5f);
        return;
    }

    //FIXME
    public override void use()
    {
        /* Steps:
            1. Do nothing, this is only for items that can be picked up
        */
        Debug.Log("no.");
        return;
    }

    // FIXME: delete when testing is done
    private void OnCollisionEnter2D(Collision2D other) {
        use();
    }
    private void OnCollisionExit2D(Collision2D other) {
        interact();
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
