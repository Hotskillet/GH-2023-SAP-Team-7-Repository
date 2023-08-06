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

        // Keep an eye out for if the player want's to pause/unpause the game
        EvtSystem.EventDispatcher.AddListener<TurnOnPauseMenu>(PauseGame);
        EvtSystem.EventDispatcher.AddListener<TurnOffPauseMenu>(ResumeGame);
    }

    public void PauseGame(TurnOnPauseMenu evt)
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame(TurnOffPauseMenu evt)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
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
}
