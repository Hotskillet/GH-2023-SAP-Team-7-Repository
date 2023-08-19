using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAround : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
