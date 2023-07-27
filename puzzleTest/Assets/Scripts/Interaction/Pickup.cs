using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Item
{
    // Start is called before the first frame update
    void Start() {
    }

    public override void interact() {

        // Step 1: Add name of item to inventory
        Inventory.Instance.AddItem(gameObject.name);

        //FIXME Step 2: Tell UI to add sprite to inventory bar
        
        AudioManager.instance.Play(soundEffect);
 
        Debug.Log(gameObject.name + " has been picked up.");


        //FIXME Step 3: do event stuff 

        // here im making a package to send through evt system
        // it holds only the comment string4
        //raises the event for the dialogue manager to pick up

        commentPackage pickupComment = new commentPackage();
        pickupComment.comment = "can someone tell me how to get the comment";
        EvtSystem.EventDispatcher.Raise(pickupComment);


        // Step 4: Delete object from world
        Destroy(gameObject, 0.5f);
        return;
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
