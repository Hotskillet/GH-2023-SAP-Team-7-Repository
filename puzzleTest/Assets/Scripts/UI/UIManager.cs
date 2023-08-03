using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // for onscreen inventory
    public Transform gameUI;

    // for pause menu
    public Transform pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        EvtSystem.EventDispatcher.AddListener<ToggleMenu>(TogglePauseMenu);
    }

    void TogglePauseMenu(ToggleMenu evt){
        if (evt.state){
            // hide game UI
            gameUI.gameObject.SetActive(false);
            // show pause menu
            pauseMenu.gameObject.SetActive(true);
        }else{
            // hide pause menu
            pauseMenu.gameObject.SetActive(false);
            // show game UI
            gameUI.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
