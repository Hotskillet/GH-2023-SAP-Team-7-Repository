using UnityEditor.Animations;
using System;
using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{
    public Animatic[] animatics;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Animatic a in animatics){
            //FIXME
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
