using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public float speed;
    public Sprite walkUpSprite;
    public Sprite walkDownSprite;
    public string walkSFX;

    public SpawnPoints[] spawnPoints;

    public float cursorSpeed;

    private bool movingUp;
    private bool movingDown;
    private bool movingLeft;
    private bool movingRight;

    private PlayerInput playerInput;

    // keep track of when player is walking so that SFX can play
    private bool isWalking;
    // keep track of when player stops walking so SFX can be stopped
    private bool wasWalking;
    // interaction input buffer
    public float interactDelay;
    private Coroutine interactBuffer;

    //FIMXE:
    // animation sprites will be in AnimationManager
    // get corresponding animation from AnimationManager.Instance.__
    private SpriteRenderer spriteRenderer;
    private Animator spriteAnimator;

    // FIXME: add SFX for player walking

    //making some custom vector3s so we dont run into transform.up issues later
    //if there's no rotation EVER, then we can use transform.up and stuff
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 right = new Vector3(1, 0, 0);

    private Vector3 movementVector;

    private GameObject itemInContact;

    private bool dialogueActive;


    public void Awake(){
        EvtSystem.EventDispatcher.AddListener<ChangePlayerPosition>(ChangePosition);
        EvtSystem.EventDispatcher.AddListener<UpdateDialogueState>(UpdateDialgoue);

        playerInput = GetComponent<PlayerInput>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteAnimator = gameObject.GetComponent<Animator>();
        ResetMovement();
        movingUp = false;
        movingDown = false;
        movingLeft = false;
        movingRight = false;
        itemInContact = null;
        interactBuffer = null;
        dialogueActive = false;
    }

    void UpdateDialgoue(UpdateDialogueState evt)
    {
        dialogueActive = evt.state;
    }


    public void ResetMovement(){
        movementVector = new Vector3(0.0f, 0.0f, 0.0f);
    }

    /*** Basic Movement ***/
    public void MoveUp(InputAction.CallbackContext context){
        if (context.performed){
            movingUp = true;
            wasWalking = false;
            isWalking = true;
        }
        if (context.canceled){
            movingUp = false;
            wasWalking = true;
            isWalking = false;
        }
    }
    public void MoveDown(InputAction.CallbackContext context){
        if (context.performed){
            movingDown = true;
            wasWalking = false;
            isWalking = true;
        }
        if (context.canceled){
            movingDown = false;
            wasWalking = true;
            isWalking = false;
        }
    }
    public void MoveLeft(InputAction.CallbackContext context){
        if (context.performed){
            movingLeft = true;
            wasWalking = false;
            isWalking = true;
        }
        if (context.canceled){
            movingLeft = false;
            wasWalking = true;
            isWalking = false;
        }
    }
    public void MoveRight(InputAction.CallbackContext context){
        if (context.performed){
            movingRight = true;
            wasWalking = false;
            isWalking = true;
        }
        if (context.canceled){
            movingRight = false;
            wasWalking = true;
            isWalking = false;
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
        if (context.performed && (itemInContact != null) && (interactBuffer == null)){
            interactBuffer = StartCoroutine(DetailedInteraction());
        }
        if (context.canceled && (itemInContact != null)){
        }
    }
    IEnumerator DetailedInteraction(){
        // check if a Pickup
        Pickup otherScript = itemInContact.GetComponent<Pickup>();
        if (otherScript != null) {
            otherScript.interact();
            yield return new WaitForSeconds(interactDelay);
            interactBuffer = null;
            yield break;
        }
        // check if a Door
        Door otherScript2 = itemInContact.GetComponent<Door>();
        if (otherScript2 != null) {
            otherScript2.unlock();
            // check for room reset script
            ResetRoom otherScript4 = itemInContact.GetComponent<ResetRoom>();
            if (otherScript4 != null){
                otherScript4.Reset();
            }
            yield return new WaitForSeconds(interactDelay);
            interactBuffer = null;
            yield break;
        }
        // check if a Container
        Container otherScript3 = itemInContact.GetComponent<Container>();
        if (otherScript3 != null) {
            otherScript3.unlock();
            yield return new WaitForSeconds(interactDelay);
            interactBuffer = null;
        }
    }
    
    public void Pause(InputAction.CallbackContext context){
        if (context.performed){
            // switch action map to "UI"
            if (playerInput != null){
                playerInput.SwitchCurrentActionMap("UI");
            }else{
                print("player input is null");
            }
            // send signal to pause game
            TurnOnPauseMenu to = new TurnOnPauseMenu {};
            if (to != null){
                EvtSystem.EventDispatcher.Raise<TurnOnPauseMenu>(to);
            }else{
                print("TurnOnPauseMenu obj is null");
            }
        }
    }
    public void Unpause(InputAction.CallbackContext context){
        if (context.performed){
            // switch action map to "Explore"
            playerInput.SwitchCurrentActionMap("Explore");
            // send signal to unpause game
            TurnOffPauseMenu to = new TurnOffPauseMenu {};
            EvtSystem.EventDispatcher.Raise<TurnOffPauseMenu>(to);
        }
    }

    public void OpenJigsawMenu(InputAction.CallbackContext context){
        if (context.performed){
            // switch action map to "UI"
            playerInput.SwitchCurrentActionMap("UI");
            // send signal to show jigsaw menu
            TurnOnJigsawMenu to = new TurnOnJigsawMenu {};
            EvtSystem.EventDispatcher.Raise<TurnOnJigsawMenu>(to);
        }
    }
    public void CloseJigsawMenu(InputAction.CallbackContext context){
        if (context.performed){
            // switch action map to "Explore"
            playerInput.SwitchCurrentActionMap("Explore");
            // send signal to unpause game
            TurnOffJigsawMenu to = new TurnOffJigsawMenu {};
            EvtSystem.EventDispatcher.Raise<TurnOffJigsawMenu>(to);
        }
    }

    // Progresses to next line of dialogue. If there are no more lines, this will close the dialouge box
    public void Next(InputAction.CallbackContext context){
        if (context.performed && dialogueActive){
            ContinueDialogue signal = new ContinueDialogue() {};
            EvtSystem.EventDispatcher.Raise<ContinueDialogue>(signal);
        }
    }

    // for jigsaw cursor movement
    public void MoveCursorUp(InputAction.CallbackContext context){
        if (context.performed){
            CursorMovement signal = new CursorMovement() {directionState = "upTrue", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }if (context.canceled){
            CursorMovement signal = new CursorMovement() {directionState = "upFalse", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }
    }
    public void MoveCursorDown(InputAction.CallbackContext context){
        if (context.performed){
            CursorMovement signal = new CursorMovement() {directionState = "downTrue", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }if (context.canceled){
            CursorMovement signal = new CursorMovement() {directionState = "downFalse", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }
    }
    public void MoveCursorLeft(InputAction.CallbackContext context){
        if (context.performed){
            CursorMovement signal = new CursorMovement() {directionState = "leftTrue", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }if (context.canceled){
            CursorMovement signal = new CursorMovement() {directionState = "leftFalse", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }
    }
    public void MoveCursorRight(InputAction.CallbackContext context){
        if (context.performed){
            CursorMovement signal = new CursorMovement() {directionState = "rightTrue", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }if (context.canceled){
            CursorMovement signal = new CursorMovement() {directionState = "rightFalse", speed = cursorSpeed};
            EvtSystem.EventDispatcher.Raise<CursorMovement>(signal);
        }
    }
    public void DragPiece(InputAction.CallbackContext context){
        if (context.performed){
            EvtSystem.EventDispatcher.Raise<TryDragPiece>(new TryDragPiece() {});
        }else if (context.canceled){
            EvtSystem.EventDispatcher.Raise<StopDragPiece>(new StopDragPiece() {});
        }
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

        // update animation
        UpdateAnimatorValues();
        // plays walking SFX only when player is actaully walking
        if (isWalking){
            AudioManager.instance.Play(walkSFX);
            isWalking = false;
        // stops walking SFX as soon as player stops walking
        }else if (wasWalking){
            AudioManager.instance.Stop(walkSFX);
            wasWalking = false;
        }
        // update position
        UpdatePosition();
    }

    // Changes the player's position when entering through a door
    public void ChangePosition(ChangePlayerPosition evt){
        Debug.Log(evt.doorName);
        foreach (SpawnPoints sp in spawnPoints){
            if (sp.enteringFrom.Equals(evt.doorName)){
                gameObject.transform.position = sp.enteringPosition;
                Debug.Log(evt.doorName);
                break;
            }
        }
    }

    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<ChangePlayerPosition>(ChangePosition);
    }
}
