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
        if (!Inventory.Instance.AddItem(gameObject.name)){
            AudioManager.instance.Play("notPickedUp"); // FIXME: make this sound?
            return;
        }

        // FIXME Step 2: Tell UI to add sprite to inventory bar
        
        AudioManager.instance.Play(soundEffect);
 
        Debug.Log(gameObject.name + " has been picked up.");

        /* THIS IS THROWING AN ERROR SO I HAD TO COMMENT IT
        // FIXME Step 3: make the popup

        ItemData objectData = ItemManager.Instance.GetData(gameObject.name);

        DialogueManager.Instance.makePopup("can someone tell me how to get the comment");
        */
        CallDialogue commentCaller = gameObject.GetComponent<CallDialogue>();
        if (commentCaller != null){
            commentCaller.ShowDialogue(gameObject.name);
        }else{
        }


        // Step 4: Delete object from world
        // unparent ToolTip first
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if ((child != null) && (child.gameObject.tag == "tipCanvas"))
            {
                child.parent = null;
                break;
            }
        }
        Destroy(gameObject, 0.5f);
        return;
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
