using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ChangeInputMap to = new ChangeInputMap {map = "Explore"};
        EvtSystem.EventDispatcher.Raise<ChangeInputMap>(to);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
