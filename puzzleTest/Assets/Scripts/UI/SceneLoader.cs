using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public Animator transition;
    public float transitionTime = 1.0f;
    
    IEnumerator LoadingRoom(string roomName){
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // go to next room
        SceneManager.LoadScene(roomName, LoadSceneMode.Additive);
    }

    public void LoadRoom(string roomName){
        StartCoroutine(LoadingRoom(roomName));
    }

    public void SaveCurrentRoom(){
        // save the current active scene before going to next
        string currRoom = SceneManager.GetActiveScene().name;
        Debug.Log("---- " + currRoom + " ----");
        if (!MySceneManager.Instance.IsSaved(currRoom)){
            MySceneManager.Instance.TrackScene(currRoom);
            Debug.Log("saved " + currRoom);
        }
        // hide current scene
        MySceneManager.Instance.HideScene(currRoom);
        Debug.Log("hid " + currRoom);
    }

    public void SaveNewRoom(string roomName){
        // check if new scene is already saved before trying to load
        if (MySceneManager.Instance.IsSaved(roomName)){
            MySceneManager.Instance.UnhideScene(roomName);
            Debug.Log("loaded " + roomName + " from save");
            return;
        }
        // track new scene
        LoadRoom(roomName);
        MySceneManager.Instance.TrackScene(roomName);
        Debug.Log("loaded " + roomName);
    }
}
