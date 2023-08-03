using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomLoader : Singleton<RoomLoader>
{
    public Animator transition;
    public float transitionTime;

    // A list of the cameras for each room 
    // (all should be disabled except the activeCamera)
    public GameObject[] roomCameras;

    // this is the starting camera
    public GameObject activeCamera;

    void Awake()
    {
        EvtSystem.EventDispatcher.AddListener<ChangeRoomStart>(DummyOut);
        EvtSystem.EventDispatcher.AddListener<ChangeRoomEnd>(DummyIn);
    }


    public GameObject CamExists(string name){
        foreach (GameObject cam in roomCameras){
            if (cam.name.Equals(name)){
                return cam;
            }
        }
        return null;
    }

    public void LoadThisRoom(ChangeRoomStart evt){
        // disable active camera
        activeCamera.SetActive(false);
        // find new camera
        GameObject newCam = CamExists(evt.roomName);
        if (newCam == null){
            Debug.Log("couldn't find " + evt.roomName);
            return;
        }
        // enable new camera
        newCam.SetActive(true);
        // save as new active camera
        activeCamera = newCam;

        // send signal to PlayerControls to update player position
        ChangePlayerPosition cr = new ChangePlayerPosition {doorName = evt.doorName};
        EvtSystem.EventDispatcher.Raise<ChangePlayerPosition>(cr);
    }

    IEnumerator TransitionOut(ChangeRoomStart evt){
        //play crossfadeOUT animation
        transition.SetTrigger("out");
        // wait
        yield return new WaitForSeconds(transitionTime);

        LoadThisRoom(evt);
    }
    void DummyOut(ChangeRoomStart evt){
        StartCoroutine(TransitionOut(evt));
    }

    IEnumerator TransitionIn(ChangeRoomEnd evt){
        Debug.Log("coming back...");
        // play crossfadeIN animation
        transition.SetTrigger("in");
        // wait
        yield return new WaitForSeconds(transitionTime);
    }
    void DummyIn(ChangeRoomEnd evt){
        StartCoroutine(TransitionIn(evt));
    }
}
