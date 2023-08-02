using UnityEditor.Animations;
using UnityEngine;


[System.Serializable]
public class Animatic
{
    public string name;
    public AnimatorController controller;

    [Range(1f, 2f)]
    public float playbackSpeed;

    public bool loop;

    [HideInInspector]
    public string trigger;
}
