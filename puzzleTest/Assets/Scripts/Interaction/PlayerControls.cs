using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public float speed;
    public Sprite walkUpSprite;
    public Sprite walkDownSprite;
    private PlayerInput playerInput;

    private bool movingUp;
    private bool movingDown;
    private bool movingLeft;
    private bool movingRight;

    //FIMXE:
    // animation sprites will be in AnimationManager
    // get corresponding animation from AnimationManager.Instance.__
    private SpriteRenderer spriteRenderer;
    private Animator spriteAnimator;

    //making some custom vector3s so we dont run into transform.up issues later
    //if there's no rotation EVER, then we can use transform.up and stuff
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 right = new Vector3(1, 0, 0);

    private Vector3 movementVector;

    private GameObject itemInContact;


    public void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteAnimator = gameObject.GetComponent<Animator>();
        ResetMovement();
        movingUp = false;
        movingDown = false;
        movingLeft = false;
        movingRight = false;
        itemInContact = null;
    }


    public void ResetMovement(){
        movementVector = new Vector3(0.0f, 0.0f, 0.0f);
    }

    /*** Basic Movement ***/
    public void MoveUp(InputAction.CallbackContext context){
        if (context.performed){
            movingUp = true;
        }
        if (context.canceled){
            movingUp = false;
        }
    }
    public void MoveDown(InputAction.CallbackContext context){
        if (context.performed){
            movingDown = true;
        }
        if (context.canceled){
            movingDown = false;
        }
    }
    public void MoveLeft(InputAction.CallbackContext context){
        if (context.performed){
            movingLeft = true;
        }
        if (context.canceled){
            movingLeft = false;
        }
    }
    public void MoveRight(InputAction.CallbackContext context){
        if (context.performed){
            movingRight = true;
        }
        if (context.canceled){
            movingRight = false;
        }
    }

    public void UpdatePosition(){
        //update the transform position
        transform.position += (movementVector * Time.deltaTime * speed);
    }


    /*** Update Values for Animator ***/
    public void UpdateAnimatorValues(){
        //getting the to be difference in x and y
        //to decide the sprite animation
        float diffX = movementVector.x;
        float diffY = movementVector.y;
        Vector2 diffVector = new Vector2(diffX, diffY);

        //update values for the animator
        spriteAnimator.SetFloat("Vertical", diffY);
        spriteAnimator.SetFloat("Speed", diffVector.sqrMagnitude);
    }


    /*** FIXME: Interaction & Pauseing ***/
    public void Interact(InputAction.CallbackContext context){
        if (context.performed && (itemInContact != null)){
            Debug.Log("other object");
            DetailedInteraction();
        }
        if (context.canceled && (itemInContact != null)){
            Debug.Log("int-end");
        }
    }
    private void DetailedInteraction(){
        // check if a Pickup
        Pickup otherScript = itemInContact.GetComponent<Pickup>();
        if (otherScript != null) {
            otherScript.interact();
            return;
        }
        // check if a Door
        Door otherScript2 = itemInContact.GetComponent<Door>();
        if (otherScript2 != null) {
            otherScript2.unlock();
            return;
        }
        // check if a Container
        Container otherScript3 = itemInContact.GetComponent<Container>();
        if (otherScript3 != null) {
            otherScript3.unlock();
            return;
        }
    }

    public void Pause(InputAction.CallbackContext context){
        //
    }


    /*** Collision Detection ***/
    private void OnCollisionEnter2D(Collision2D other) {
        Item otherScript = other.gameObject.GetComponent<Item>();
        if (otherScript != null){
            itemInContact = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        itemInContact = null;
    }


    // Update
    private void Update(){
        // resets movementVector to (0,0,0)
        ResetMovement();

        // Upward movement is prioritized over downward movement
        // Not sure how to make them cancel each other...
        if (movingUp){
            movementVector += up;
        }else if (movingDown){
            movementVector += down;
        }

        // Leftward movement is prioritized over rightward movement
        // Not sure how to make them cancel each other...
        if (movingLeft){
            movementVector += left;
        }else if (movingRight){
            movementVector += right;
        }

        // update animation
        UpdateAnimatorValues();
        // update position
        UpdatePosition();
    }
}
