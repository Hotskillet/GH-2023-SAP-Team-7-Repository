using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public Animator transition;
    public float transitionTime = 1.0f;

    IEnumerator LoadingRoom(int roomNumber){
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // go to next room
        SceneManager.LoadScene(roomNumber);
    }
    IEnumerator LoadingRoom(string roomName){
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // go to next room
        SceneManager.LoadScene(roomName);
    }

    public void LoadRoom(int roomNumber){
        StartCoroutine(LoadingRoom(roomNumber));
    }
    public void LoadRoom(string roomName){
        StartCoroutine(LoadingRoom(roomName));
    }

    public void LoadNextRoom(){
        StartCoroutine(LoadingRoom(SceneManager.GetActiveScene().buildIndex + 1));
    }
}
