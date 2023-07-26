using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FSM
0 = no snap
1 = snap
*/

public class Snap : MonoBehaviour
{
    public Vector3 connectionDist;
    private DragDrop dragFunction;

    private int state;
    private bool connected;

    // Start is called before the first frame update
    void Start()
    {
        dragFunction = gameObject.transform.parent.GetComponent<DragDrop>();
        state = 0;
        connected = false;
    }

    // add colliders that enter into a list
    void OnTriggerEnter2D(Collider2D other) {
    }

    void OnTriggerStay2D(Collider2D other){
    }

    // remove any collide from the list if they are moved outside of the hitbox
    void OnTriggerExit2D(Collider2D other) {
    }

    void ExecuteStates(){
        switch (state){
            case 0: // unpair this gameOject from the other (won't follow anymore)
                break;
            case 1: // pair this gameOject with the other (will follow like connected as one)
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
