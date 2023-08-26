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
    public string nextRoom;

    public void unlock(){
        // call Unlockable.interact()
        if (locked){
            base.interact();
        }
        // go to next room if door is unlocked
        if (!locked) {
            AudioManager.instance.Play(soundEffect);
            //MySceneManager.Instance.LoadThisRoom(nextRoom);
            ChangeRoomStart cr = new ChangeRoomStart {roomName = nextRoom, doorName = gameObject.name};
            EvtSystem.EventDispatcher.Raise<ChangeRoomStart>(cr);
            // BGM
            SwitchBGM script = gameObject.GetComponent<SwitchBGM>();
            if (script != null)
            {
                script.ChangeBGM();
            }
        }
    } 
}
