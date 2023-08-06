using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2D : MonoBehaviour
{
    //making some custom vector3s so we dont run into transform.up issues later
    //if there's no rotation EVER, then we can use transform.up and stuff
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 right = new Vector3(1, 0, 0);

    public float speed = 5.0f;    

    public GameObject gangy;

    private SpriteRenderer spriteRenderer;
    private Animator spriteAnimator;

    public Sprite walkUpSprite;
    public Sprite walkDownSprite;




    // Start is called before the first frame update
    void Start() {
        spriteRenderer = gangy.GetComponent<SpriteRenderer>();
        spriteAnimator = gangy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        //movemeent stuff starts here
        //create empty movement displacmeent vector
        Vector3 movementVector = new Vector3(0.0f, 0.0f, 0.0f);


        //if input, add respective direction vector
        if (Input.GetKey("w")) {
            movementVector += up;
        }
        
        if (Input.GetKey("s")) {
            movementVector += down;
        }
        
        if (Input.GetKey("a")) {
            movementVector += left;
        }
        
        if (Input.GetKey("d")) {
            movementVector += right;
        }
        
        //getting the to be difference in x and y
        //to decide the sprite animation
        float diffX = movementVector.x;
        float diffY = movementVector.y;

        Vector2 diffVector = new Vector2(diffX, diffY);


        //update values for the animator
        spriteAnimator.SetFloat("Vertical", diffY);
        spriteAnimator.SetFloat("Speed", diffVector.sqrMagnitude);

        //update the transform position
        transform.position += (movementVector * Time.deltaTime * speed);
        
    }
}
