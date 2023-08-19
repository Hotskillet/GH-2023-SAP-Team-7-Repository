using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;

        // Keep an eye out for if the player want's to pause/unpause the game
        EvtSystem.EventDispatcher.AddListener<TurnOnPauseMenu>(PauseGame);
        EvtSystem.EventDispatcher.AddListener<TurnOffPauseMenu>(ResumeGame);
    }

    public void PauseGame(TurnOnPauseMenu evt)
    {
        if (pauseMenu != null){
            pauseMenu.SetActive(true);
        }else{
            print("pause menu is null");
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame(TurnOffPauseMenu evt)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void ResumeGame2()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        ChangeInputMap to = new ChangeInputMap {map = "Explore"};
        EvtSystem.EventDispatcher.Raise<ChangeInputMap>(to);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuUI");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<TurnOnPauseMenu>(PauseGame);
        EvtSystem.EventDispatcher.RemoveListener<TurnOffPauseMenu>(ResumeGame);
    }
}
