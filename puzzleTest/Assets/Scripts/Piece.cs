using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int[] coordinates;
    public int[] sideID;

    //FIXME: add queue for collider signals
    
    // Start is called before the first frame update
    void Start()
    {
        sideID = new int[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
