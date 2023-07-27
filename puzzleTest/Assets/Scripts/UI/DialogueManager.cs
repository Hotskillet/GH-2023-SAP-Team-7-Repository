using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// The purpose of this script:
// to listen for when a player interacts with an object
// at which point open up the dialogue.
// the game then freezes player actions 
// the player can close the dialogue and continue with the game
public class DialogueManager : MonoBehaviour
{

    //selecting the player thru the insepector
    public GameObject player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        // use getComponent to get the rigidbody 
        rb = player.GetComponent<Rigidbody2D>();


        // add an event listener for the comment package
        EvtSystem.EventDispatcher.AddListener<commentPackage>(makeDialoguePopup);

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // this function is made to 
    // freeze the player
    // make a dialogue popup
    // let the player close the popup
    // unfreeze player and close popup
    // it is designed to do this from the event system
    void makeDialoguePopup(commentPackage grub) {
        
        //Step one: Freeze the player
        // rb.constraints = RigidbodyConstraints2D.FreezePosition;

        Debug.Log(grub);
    }
}
