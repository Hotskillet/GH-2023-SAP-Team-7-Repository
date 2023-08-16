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

    // to be able to check whether the calculations within the corutine are completed
    private Coroutine fadeRoutine;


    void Awake()
    {
        EvtSystem.EventDispatcher.AddListener<ChangeRoomStart>(TryFade);
    }

    void Update(){
        if (fadeRoutine == null){
            print("done");
        }else{
            print("working...");
        }
    }


    public GameObject CamExists(string name){
        foreach (GameObject cam in roomCameras){
            if (cam.name.Equals(name)){
                return cam;
            }
        }
        return null;
    }

    void TryFade(ChangeRoomStart evt)
    {
        if (fadeRoutine == null){
            fadeRoutine = StartCoroutine(RoomTransition(evt));
        }
    }

    IEnumerator RoomTransition(ChangeRoomStart evt)
    {
        // fade out
        transition.SetTrigger("out");
        yield return new WaitForSeconds(transitionTime);

        // FIXME: do logic
        // disable active camera
        activeCamera.SetActive(false);
        // find new camera
        GameObject newCam = CamExists(evt.roomName);
        if (newCam == null){
            Debug.Log("couldn't find " + evt.roomName);
            // fade in
            transition.SetTrigger("in");
            yield break;
        }
        // enable new camera
        newCam.SetActive(true);
        // save as new active camera
        activeCamera = newCam;
        // send signal to PlayerControls to update player position
        EvtSystem.EventDispatcher.Raise<ChangePlayerPosition>(new ChangePlayerPosition {doorName = evt.doorName});

        // fade in
        transition.SetTrigger("in");
        yield return new WaitForSeconds(transitionTime);

        // clear var
        fadeRoutine = null;
    }

    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<ChangeRoomStart>(TryFade);
    }
}
