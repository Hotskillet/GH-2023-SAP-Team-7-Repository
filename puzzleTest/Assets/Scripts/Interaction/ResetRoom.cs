using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRoom : MonoBehaviour
{
    // a prefab of the room
    public GameObject defaultState;
    // a reference to the actual room or components
    public GameObject currentState;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Reset()
    {
        // instantiate new room
        GameObject newRoom = Instantiate(defaultState, currentState.transform.position, Quaternion.identity);
        // destroy current room
        Destroy(currentState, 0.1f);
        // reference new instantiation as current room
        currentState = newRoom;
    }
}
