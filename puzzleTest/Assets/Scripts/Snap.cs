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
    public bool connected;
    
    private DragDrop dragFunction;
    private Transform parentPiece;

    // Start is called before the first frame update
    void Start()
    {
        dragFunction = gameObject.transform.parent.GetComponent<DragDrop>();
        parentPiece = gameObject.transform.parent;
    }

    public void Init(){
        dragFunction = gameObject.transform.parent.GetComponent<DragDrop>();
        parentPiece = gameObject.transform.parent;
    }

    bool isDifferentPiece(GameObject other){
        return (other.GetComponent<Snap>() != null) && (other.transform != parentPiece);
    }

    // add colliders that enter into a list
    void OnTriggerEnter2D(Collider2D other)
    {
        // to make sure the piece's own collider isn't triggering this
        // and that the piece has been released
        // and that the piece is not already connected
        if (isDifferentPiece(other.gameObject) && dragFunction.justReleased && !connected)
        {
            Debug.Log("enter");
            // once piece is released, child this object to the other
            gameObject.transform.parent = other.gameObject.transform;
            connected = true;
        }
    }

    // remove any collide from the list if they are moved outside of the hitbox
    void OnTriggerExit2D(Collider2D other)
    {
        // to make sure the piece's own collider isn't triggering this
        // and that the piece has been released
        // and that the piece is not already connected
        if (isDifferentPiece(other.gameObject) && dragFunction.justReleased && connected)
        {
            Debug.Log("exit");
            // once piece is released, child this object to the other
            gameObject.transform.parent = null;
            connected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
