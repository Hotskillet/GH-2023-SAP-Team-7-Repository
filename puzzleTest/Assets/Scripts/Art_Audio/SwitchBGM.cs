using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBGM : MonoBehaviour
{
    public string switchTo;
    public string switchFrom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeBGM()
    {
        AudioManager.instance.Stop(switchFrom);
        AudioManager.instance.Play(switchTo);
    }
}
