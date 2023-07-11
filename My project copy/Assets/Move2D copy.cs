using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{ 
    public int myInt;
    //the changable speed variable.
    public float speed = 1.0f;
    //changleable jump variable
    public float jumpStrength = 10.0f;
    private Rigidbody2D rb;
    private int layerMask;

    //Start is called before the first frame update
    void Start()
    { 
        myInt = 0;

        rb = gameObject.GetComponent<Rigidbody2D>();
        layerMask = LayerMask.GetMask("Ground");
    }

    bool isGrounded()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = Vector2.down;
        float distance = 0.7f; 

        return Physics2D.Raycast(origin, direction, distance, layerMask);
    }
    // Update is called once per frame
    void Update() 
        {
            
            //movementation. 
            Vector3 movDir = new Vector3(0.0f, 0.0f, 0.0f);

            if (Input.GetKey(KeyCode.A))  
            {
            movDir = -transform.right;
            }
        else if (Input.GetKey(KeyCode.D))  
            {
            movDir = transform.right;
            }

        gameObject.transform.Translate(speed * movDir * Time.deltaTime);

        //the jump code, when it works. 
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded())
        {
        Vector3 jumpForce = new Vector3(0.0f, jumpStrength, 0.0f);

        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }
}
