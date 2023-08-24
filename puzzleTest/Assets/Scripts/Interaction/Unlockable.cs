using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// mel can you explain what this line says?
public class Unlockable : Item
{
    public bool locked;

    private bool foundKey;

    // Start is called before the first frame update
    void Start()
    {
        foundKey = false;
    }

    public bool GetFoundKey(){
        return foundKey;
    }


    public override void interact()
    {
        // skip steps if already unlocked
        if (!locked){
            return;
        }
        /* Steps:
            1. Check if Player's inventory has item that matches an item in this item's list
            2. update state
            4. disable this object
        */
        // Step 1: Check if Player's inventory has item that matches an item in this item's list
        ItemData temp = ItemManager.Instance.GetData(gameObject.name);
        if (temp == null){
            Debug.Log("Item not in database");  //FIXME: UI & Narrative
            return;
        }
        foreach (string possibleMatch in temp.canInteractWith){
            if (Inventory.Instance.findItemIndex(possibleMatch) != -1){
                // Step 2: Update state
                locked = false;
                foundKey = true;
                // SKIPPING: Steap 3: Disable this object's rigidbody
                /*
                Rigidbody2D rbody = gameObject.GetComponent<Rigidbody2D>();
                rbody.simulated = false;
                */
                //FIXME: replace with animation & SFX
                SpriteRenderer thisSprite = gameObject.GetComponent<SpriteRenderer>();
                Color currCol = thisSprite.color;
                thisSprite.color = new Color(currCol.r, currCol.g, currCol.b, currCol.a * 0.1f);
                //FIXME: remove item from inventory?
                Inventory.Instance.RemoveItem(possibleMatch);
                return;
            }
        }
        Debug.Log("You don't have the correct item to open this.");  //FIXME: locked door sound?
        return;
    }
}
