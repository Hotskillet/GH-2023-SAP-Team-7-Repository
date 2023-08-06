using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : Singleton<SceneLoader>
{
    // Start is called before the first frame update
    void Start()
    {
    }
    
    // this goes to the next scene
    // check "File > Build Settings" for the scene order
    public void GoToNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // this goes to a specified scene (name only)
    public void GoToScene(string name){
        SceneManager.LoadScene(name);
    }
    // this goes to a specified scene (build index only)
    // check "File > Build Settings" to see the build indices of the scenes
    public void GoToScene(int index){
        SceneManager.LoadScene(index);
    }
}
