using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //this adds a listener to listen for foundmatch
        EvtSystem.EventDispatcher.AddListener<foundMatch>(receiveThing);
    }

    // Update is called once per frame
    void Update()
    {

        //this creates a new evnt of class type foundmatch
        //then it sounds out evnt with the type foundmatch 
        //anything listening for foundmatch class stuff will receive

        foundMatch evnt = new foundMatch();
        evnt.distanceThing = 10.0f;

        EvtSystem.EventDispatcher.Raise(evnt);
    }

    void receiveThing(foundMatch evntPackageThing) {

        print(evntPackageThing.distanceThing);

    }

}
