using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    public float delay;

void Start(){
  // this will run as soon as scene is loaded
  StartCoroutine(DelayedReturn(delay));
}

IEnumerator DelayedReturn(float d){
  // wait for a bit so player can see "Thanks for playing!"
  yield return new WaitForSeconds(d);
  // go back to main menu
  // SceneLoader.Instance.GoToScene("MainMenuUI"); old code in case it breaks - de'jon
  SceneLoader.Instance.GoToScene("Credits Roll");
}
}
