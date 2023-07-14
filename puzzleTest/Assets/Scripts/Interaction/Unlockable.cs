using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : Item
{
    public bool locked;
    // an item contained in this object will be a child
    public GameObject treasurePrefab;

    // Start is called before the first frame update
    void Start()
    {
        locked = true;
    }

    public override void interact(){
        /* Steps:
            1. Do nothing, this is only for items that can be picked up
        */
        Debug.Log("no.");
        return;
    }

    public override void use()
    {
        /* Steps:
            1. Check if Player's inventory has item that matches an item in this item's list
            2. update state
            3. enable treasure
            4. disable this object
        */
        // Step 1: Check if Player's inventory has item that matches an item in this item's list
        ItemData temp = ItemManager.Instance.GetData(gameObject.name);
        if (temp == null){
            Debug.Log("Item not in database");
            return;
        }
        foreach (string possibleMatch in temp.canInteractWith){
            if (Inventory.Instance.findItemIndex(possibleMatch) != -1){
                // Step 2: Update state
                locked = false;
                Debug.Log(possibleMatch + " opened " + gameObject.name);
                //FIXME: remove item from inventory?
                Inventory.Instance.RemoveItem(possibleMatch);
                // Step 3: Enable treasure
                if (treasurePrefab != null){
                    Instantiate(treasurePrefab, gameObject.transform.position, Quaternion.identity);
                }
                // Steap 4: Disable this object
                gameObject.SetActive(false);
                return;
            }
        }
        Debug.Log("You don't have the correct item to open this.");
        return;
    }

    /*
    // FIXME: remove since Player will be calling these functions
    private void OnCollisionEnter2D(Collision2D other) {
        interact();
    }
    // FIXME: remove since Player will be calling these functions
    private void OnCollisionExit2D(Collision2D other) {
        use();
    }
    */

    // Update is called once per frame
    void Update()
    {
    }
}
