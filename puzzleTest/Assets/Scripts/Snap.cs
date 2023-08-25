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

    private GameObject currentlyTouching;

    private Transform defaultParent;

    // Start is called before the first frame update
    void Start()
    {
        dragFunction = gameObject.transform.parent.GetComponent<DragDrop>();
        parentPiece = gameObject.transform.parent;
        defaultParent = null;
    }

    public void SetDefaultParent(Transform parent)
    {
        defaultParent = default;
        return;
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
        /* save ref to currentlyTouching */
        if (isDifferentPiece(other.gameObject) && !connected && (dragFunction.state == 1)){
            currentlyTouching = other.gameObject;
            Debug.Log("load");
        }
        
        /*
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
        */
    }

    // remove any collide from the list if they are moved outside of the hitbox
    void OnTriggerExit2D(Collider2D other)
    {
        /* clear currentlyTouching */
        if ((currentlyTouching != null) && isDifferentPiece(other.gameObject) && (dragFunction.state == 1)){
            currentlyTouching = null;
            Debug.Log("clear");
        }
        /* disconnect if connection is moved away and piece is connected to something */
        if (connected && isDifferentPiece(other.gameObject)){
            gameObject.transform.parent.transform.parent = defaultParent;
            connected = false;
        }
        /*
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
        */
    }

    // Update is called once per frame
    void Update()
    {
        if ((currentlyTouching != null) && dragFunction.justReleased && !connected){
            // FIXME: do snapping
            // parent dragged piece to other peice
            Transform thisParent = gameObject.transform.parent;
            Transform otherParent = currentlyTouching.transform.parent;
            thisParent.transform.parent = otherParent.transform;
            // move dragged piece to proper location
            Vector3 otherDist = currentlyTouching.GetComponent<Snap>().connectionDist;
            thisParent.transform.position = otherParent.transform.position + otherDist;
            Debug.Log("SNAP");
            connected = true;
        }
    }
}
