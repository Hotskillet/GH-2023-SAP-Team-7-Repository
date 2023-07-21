using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    private Dictionary<string, Scene> savedScenes;

    // Start is called before the first frame update
    void Start()
    {
        savedScenes = new Dictionary<string, Scene>();
    }

    public bool IsSaved(string name){
        Scene value;
        return savedScenes.TryGetValue(name, out value);
    }
    
    public void TrackScene(string name){
        Scene value;
        if (!savedScenes.TryGetValue(name, out value)){
            savedScenes.Add(name, SceneManager.GetActiveScene());
        }
    }
    public void UntrackScene(string name){
        Scene value;
        if (savedScenes.TryGetValue(name, out value)){
            savedScenes.Remove(name);
        }
    }

    // This MUST be called AFTER checking if the name is a key in the dictionary
    // because Unity doesn't allow Scene variables to have a null value (so 
    // we can't return null).
    public Scene GetScene(string name){
        Scene value;
        savedScenes.TryGetValue(name, out value);
        return value;
    }

    // manage hiding objects in each scene
    public void HideScene(string name){
        Scene curr;
        // if scene isn't saved, return
        if (!savedScenes.TryGetValue(name, out curr)){
            return;
        }
        //FIXME: hide scene
        GameObject[] sceneObjects = curr.GetRootGameObjects();
        foreach (GameObject go in sceneObjects){
            go.SetActive(false);
        }
    }

    // manage unhiding objects in each scene
    public void UnhideScene(string name){
        Scene curr;
        // if scene isn't saved, return
        if (!savedScenes.TryGetValue(name, out curr)){
            return;
        }
        //FIXME: hide scene
        GameObject[] sceneObjects = curr.GetRootGameObjects();
        foreach (GameObject go in sceneObjects){
            go.SetActive(true);
        }
    }
}
