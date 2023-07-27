using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ExampleTrigger : EvtSystem.Event
{
    public string data1;
    public float data2;
}

// dont delete this or snapper pls
// bao doesnt really know how to use event dispatcher
// he needs this until hes used to it
public class hit : EvtSystem.Event
{
    public float distanceThing;
}

// change room for switching rooms.
// variables are self explanatory
public class ChangeRoom : EvtSystem.Event
{
    public string roomName;
    public string doorName;
}


public class commentPackage : EvtSystem.Event {

    // this is the comment in the interactable
    public string comment;

}