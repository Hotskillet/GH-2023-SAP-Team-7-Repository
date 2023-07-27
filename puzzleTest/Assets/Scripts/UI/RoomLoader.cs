using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomLoader : Singleton<RoomLoader>
{
    public Animator transition;
    public float transitionTime = 1.0f;

    // A list of the cameras for each room 
    // (all should be disabled except the activeCamera)
    public GameObject[] roomCameras;

    // this is the starting camera
    public GameObject activeCamera;

    void Awake()
    {
        EvtSystem.EventDispatcher.AddListener<ChangeRoom>(LoadThisRoom);
    }


    IEnumerator TransitionExit(){
        //FIXME: play crossfadeOUT animation
        transition.SetTrigger("Start");
        // wait
        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator TransitionEnter(){
        // wait
        yield return new WaitForSeconds(transitionTime);
        //FIXME: play crossfadeIN animation
        transition.SetTrigger("Start");
    }

    public GameObject CamExists(string name){
        foreach (GameObject cam in roomCameras){
            if (cam.name.Equals(name)){
                return cam;
            }
        }
        return null;
    }

    public void LoadThisRoom(ChangeRoom evt){
        // start transition animation
        StartCoroutine(TransitionExit());
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
        // end transition animation
        StartCoroutine(TransitionEnter());
    }
}
