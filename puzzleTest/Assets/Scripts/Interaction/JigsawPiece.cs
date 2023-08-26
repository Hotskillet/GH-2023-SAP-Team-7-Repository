using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPiece : Pickup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public override void interact() {
         //FIXME Step 2: Tell UI to add sprite to inventory bar
        
        AudioManager.instance.Play(soundEffect);

        // PLEASE FOR THE LOVE OF GOD THIS NEEEEEEEEEEEDS TO BE IN THE GAME 
        Debug.Log(gameObject.name + " has been found.");

        // Step 3: Delete object from world
        // add to puzzle menu
        EvtSystem.EventDispatcher.Raise<FoundAPiece>(new FoundAPiece {});
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
        Destroy(gameObject, destroyDelay);
    }
}
