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

public class TurnOnPauseMenu : EvtSystem.Event
{
}
public class TurnOffPauseMenu : EvtSystem.Event
{
}
public class ChangeInputMap : EvtSystem.Event
{
    public string map;
}

public class TurnOnJigsawMenu : EvtSystem.Event
{
}
public class TurnOffJigsawMenu : EvtSystem.Event
{
}
public class ChangeParent : EvtSystem.Event
{
    public Transform newParent;
}

public class LoadPieces : EvtSystem.Event
{
}
public class FoundAPiece : EvtSystem.Event
{
}

public class commentPackage : EvtSystem.Event {

    // this is the comment in the interactable
    public string comment;

}

public class ShowInteractTip : EvtSystem.Event
{
    public string info;
    public Transform obj;
}
public class HideInteractTip : EvtSystem.Event
{
}

public class UpdateDialogueState : EvtSystem.Event
{
    public bool state;
}
public class ContinueDialogue : EvtSystem.Event
{
}
public class UpdateNewLines : EvtSystem.Event
{
    public string[] moreLines;
}

public class TryDragPiece : EvtSystem.Event
{
}
public class StopDragPiece : EvtSystem.Event
{
}
public class CursorMovement : EvtSystem.Event
{
    public string directionState;
    public float speed;
}