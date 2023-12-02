using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEndingScript : MonoBehaviour
{
    public float delay;
    public string switchTo;
    public string switchFrom;
    public string scenename;

    void Start(){
        StartCoroutine(DelayedReturn(delay));
        AudioManager.instance.Stop(switchFrom);
        AudioManager.instance.Play(switchTo);
}

IEnumerator DelayedReturn(float d){
  // wait for a bit so player can see "Thanks for playing!"
  yield return new WaitForSeconds(d);
  // go back to main menu
  // SceneLoader.Instance.GoToScene("MainMenuUI"); old code in case it breaks - de'jon - SceneLoader.Instance.GoToScene("Intro");
  AudioManager.instance.Stop("CreditsMusic");
  SceneManager.LoadScene(scenename);
}
}

