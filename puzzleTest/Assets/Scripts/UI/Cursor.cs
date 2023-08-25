using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public float speed;
    public Vector3 hoverFactor;

    public int state;
    private bool held;
    public bool justReleased;
    private bool inside;

    private GameObject thingBeingDragged;

    private Vector3 movementVector;
    private bool movingUp;
    private bool movingDown;
    private bool movingLeft;
    private bool movingRight;
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 right = new Vector3(1, 0, 0);


    void Awake()
    {
        EvtSystem.EventDispatcher.AddListener<TryDragPiece>(StartDrag);
        EvtSystem.EventDispatcher.AddListener<StopDragPiece>(StopDrag);
        EvtSystem.EventDispatcher.AddListener<CursorMovement>(MoveCursor);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        held = false;
        justReleased = false;
        thingBeingDragged = null;
        movingUp = false;
        movingDown = false;
        movingLeft = false;
        movingRight = false;
    }


    void ResetMovement(){
        movementVector = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void UpdatePosition(){
        //update the transform position
        transform.position += (movementVector * Time.deltaTime * speed);
    }

    void MoveCursor(CursorMovement evt)
    {
        switch (evt.directionState)
        {
            case "upTrue":
                movingUp = true;
                break;
            case "upFalse":
                movingUp = false;
                break;
            case "downTrue":
                movingDown = true;
                break;
            case "downFalse":
                movingDown = false;
                break;
            case "leftTrue":
                movingLeft = true;
                break;
            case "leftFalse":
                movingLeft = false;
                break;
            case "rightTrue":
                movingRight = true;
                break;
            case "rightFalse":
                movingRight = false;
                break;
        }
    }

    public void StartDrag(TryDragPiece evt)
    {
        if (inside)
        {
            held = true;
            justReleased = false;
        }
    }
    public void StopDrag(StopDragPiece evt)
    {
        if (!inside)
        {
            justReleased = true;
            held = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        print("uh");
        if (other.gameObject.tag == "jigsaw")
        {
            print("hello");
            inside = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // UI: scale-up object
        other.transform.localScale = new Vector3(other.transform.localScale.x * hoverFactor.x,
            other.transform.localScale.y * hoverFactor.y, 
            other.transform.localScale.z * hoverFactor.z);
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "jigsaw")
        {
            inside = true;
            other.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    void Update() {
        // cursor movement
        ResetMovement();

        // apply up/down movement
        if (movingUp){
            movementVector += up;
        }
        if (movingDown){
            movementVector += down;
        }

        // apply left/right movement
        if (movingLeft){
            movementVector += left;
        }
        if (movingRight){
            movementVector += right;
        }

        // update position
        UpdatePosition();

        /* 
        Update State
            Allows object to follow mouse even if mouse moves outside of it (as long as left-click
            is still being held down)
        */
        if (inside && held){
            state = 1;
        }else if (!held){
            state = 0;
        }

        switch (state){
            case 0:
                break;
            case 1:
                // make object follow mouse
                thingBeingDragged.transform.position = gameObject.transform.position;
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<TryDragPiece>(StartDrag);
        EvtSystem.EventDispatcher.RemoveListener<StopDragPiece>(StopDrag);
        EvtSystem.EventDispatcher.RemoveListener<CursorMovement>(MoveCursor);
    }
}
