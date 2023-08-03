using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// The purpose of this script:
// to listen for when a player interacts with an object
// at which point open up the dialogue.
// the game then freezes player actions 
// the player can close the dialogue and continue with the game
public class Dialogue : Singleton<Dialogue>
{

    // need:
    // get the dialogue box
    // get its text boxes
    public GameObject dialogueBox;
    public GameObject dialogueTMP;
    private TMP_Text TextComponent;

    // Start is called before the first frame update
    void Start()
    {
        TextComponent = dialogueTMP.GetComponent<TMP_Text>();
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
    void makePopup(string grub) {
        
        // Step two: make the dialogue box appear
        dialogueBox.SetActive(true);
        // Step three: put grub text on it
        TextComponent.text = grub;
        // Step four: make it so that you can close it

        
        Debug.Log(grub);

    }

    void closePopup() {

    }
}