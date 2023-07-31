using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ExampleTrigger : EvtSystem.Event
{
    public string data1;
    public float data2;
}

public class hit : EvtSystem.Event
{
    public float distanceThing;
}

public class ChangeRoomStart : EvtSystem.Event
{
    public string roomName;
    public string doorName;
}
public class ChangePlayerPosition : EvtSystem.Event
{
    public string doorName;
}
public class ChangeRoomEnd : EvtSystem.Event
{
}
