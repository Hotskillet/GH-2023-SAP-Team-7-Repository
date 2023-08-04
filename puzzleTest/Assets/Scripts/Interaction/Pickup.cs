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

        //FIXME Step 2: Tell UI to add sprite to inventory bar
        
        AudioManager.instance.Play(soundEffect);

        // PLEASE FOR THE LOVE OF GOD THIS NEEEEEEEEEEEDS TO BE IN THE GAME 
        Debug.Log(gameObject.name + " has been picked up.");

        // Step 3: Delete object from world
        Destroy(gameObject, 0.5f);
        return;
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
