using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// attach this to an item and fill in whatever unique tip it has
// All tool tips have the tip of "Press F to interact" (Keyboard) or "Press A to interact" (XBOX)
public class ToolTip : MonoBehaviour
{
    public string usefulInfo;

    private bool signalRaised;


    void Start(){
        signalRaised = false;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            signalRaised = true;
            ShowInteractTip signal = new ShowInteractTip() {info = usefulInfo};
            EvtSystem.EventDispatcher.Raise<ShowInteractTip>(signal);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (signalRaised)
        {
            signalRaised = false;
            HideInteractTip signal = new HideInteractTip() {};
            EvtSystem.EventDispatcher.Raise<HideInteractTip>(signal);
        }
    }
}
