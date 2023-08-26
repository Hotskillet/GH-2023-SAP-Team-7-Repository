using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Pickup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void interact(){
        // do regular pickup interactions
        base.interact();
        // go to end game cutscene
        SceneLoader.Instance.GoToNextScene();
    }
}
