using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogue : MonoBehaviour
{

    public void ShowDialogue(string objName)
    {
        // get comment data for item
        ItemData thisData = ItemManager.Instance.GetData(objName);
        if (thisData == null){
            return;
        }
        // call Dialuge using comment data from item data
        if (thisData.comments.Length > 0){
            EvtSystem.EventDispatcher.Raise<UpdateNewLines>(new UpdateNewLines() {moreLines = thisData.comments});
        }
    }
}
