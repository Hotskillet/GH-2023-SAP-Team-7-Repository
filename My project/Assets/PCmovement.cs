using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCmovement : MonoBehaviour
{ 
    public int myInt;
    public float speed = 1.0f;
    private Rigidbody2D rb;

    //Start is called before the first frame update
    void Start()
    { 
       
    }
    // Update is called once per frame
    void Update() 
        {
            
            //movement
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
            

        }
}

