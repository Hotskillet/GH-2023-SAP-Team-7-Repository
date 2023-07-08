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
    private int prevState;
    private int state;
    private Queue<Collider2D> collisionQueue;

    // Start is called before the first frame update
    void Start()
    {
        dragFunction = gameObject.transform.parent.GetComponent<DragDrop>();
        prevState = 0;
        state = 0;
        collisionQueue = new Queue<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //FIXME: check for correctness here
        if (state == 1){ // only the piece being dragged should snap itself to the other
            collisionQueue.Enqueue(other);
            Debug.Log("collision");
        }
    }

    void ExecuteStates(){
        switch (state){
            case 0: // unpair this gameOject from the other (won't follow anymore)
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                gameObject.transform.parent.transform.parent = null;
                break;
            case 1: // pair this gameOject with the other (will follow like connected as one)
                Collider2D other = collisionQueue.Dequeue();
                /*
                This connection is the child of the piece. We need to update the parent of the
                piece (aka the parent of the parent)
                */
                gameObject.transform.parent.transform.parent = other.gameObject.transform.parent.transform.parent;
                gameObject.transform.parent.transform.parent.position += connectionDist;
                collisionQueue.Clear();
                //
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // update state
        if ((dragFunction.justReleased) && (collisionQueue.Count > 0)){
            prevState = state;
            state = 1;
        }else if (dragFunction.state == 1){
            prevState = state;
            state = 0;
        }
        // check if state changed
        if (prevState != state){
            ExecuteStates();
        }
    }
}
