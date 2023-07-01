using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{

    //when the mopuse is dropped, we check if the hitbox is active
    //if it is, check for other hitboxes
    //check the criteria:
    //correct sides
    //correct side id's (add id's together)
    //then mark this hitbox as HITTING 
    //then we send out signals to all the sibling goobers for confirmation
    //once we receive all the confirmation, then snap.
    //what do we mean by confirmation?
    //if any of the others are ALSO hits and are closer, then cancel.
    //so if this goober receives 3 confirmations back, then snap.
    //otherwise, dont snap.

    // Start is called before the first frame update
    void Start()
    {
        //this adds a listener to listen for foundmatch
        EvtSystem.EventDispatcher.AddListener<hit>(receiveHit);
    }

    // Update is called once per frame
    void Update()
    {

        //this creates a new evnt of class type foundmatch
        //then it sounds out evnt with the type foundmatch 
        //anything listening for foundmatch class stuff will receive

        hit evnt = new hit();
        evnt.distanceThing = 10.0f;

        EvtSystem.EventDispatcher.Raise(evnt);
    }


    //this function is for receviing hit signals from other goobers
    void receiveHit(hit evntPackageThing) {

        print(evntPackageThing.distanceThing);

    }




}