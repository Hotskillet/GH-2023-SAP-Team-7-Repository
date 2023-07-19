using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// doors store the layout (which room to swap to) and the coordinates 
// or swap to a different scene
// please do this
// actually i just remembered what rebecca said
// USE ROOM PREFABS (WITH ITEMS )PLEASEEEEEEEE

public class Door : Unlockable
{
    // the scene number of the room the door is supposed to link to
    public int nextRoom;

    public void Start() 
    {
        locked = true;
    }

    private void unlock(){
        // call Unlockable.interact()
        base.interact();
    }    

    // FIXME: remove since Player will be calling these functions
    private void OnCollisionExit2D(Collision2D other) {
        unlock();
    }


}
