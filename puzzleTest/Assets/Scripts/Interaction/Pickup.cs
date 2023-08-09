using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Item
{
    public float destroyDelay;

    
    // Start is called before the first frame update
    void Start() {
    }

    public override void interact() {

        // Step 1: Add name of item to inventory
        Inventory.Instance.AddItem(gameObject.name);

        // FIXME Step 2: Tell UI to add sprite to inventory bar
        
        AudioManager.instance.Play(soundEffect);
 
        Debug.Log(gameObject.name + " has been picked up.");


        // FIXME Step 3: make the popup

        ItemData objectData = ItemManager.Instance.GetData(gameObject.name);

        // DialogueManager.Instance.makePopup("can someone tell me how to get the comment");


        // Step 4: Delete object from world
        Destroy(gameObject, destroyDelay);
        return;
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
