using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutscenePlayer : MonoBehaviour
{
    // set the animations trigger to be called "Start"
    public Animator cutscene;

    // Start is called before the first frame update
    void Start()
    {
        PlayCutscene();
    }

    void PlayCutscene(){
        cutscene.SetTrigger("Start");
    }
}
