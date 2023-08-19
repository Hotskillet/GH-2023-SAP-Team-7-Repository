using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPush : MonoBehaviour
{

    // gameObjects rigidbody
    private Rigidbody2D rb;


    // when touches something
    void OnCollisionEnter2D(Collision2D collision) {

        // check if its the player
        if (collision.gameObject.tag == "Player") {

            //if it is the player then make this box movable 
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

    }

    // when what ever was touching it (typically the player) stops touching it,
    void OnCollisionExit2D(Collision2D collision) {

            // make box not movable
            rb.bodyType = RigidbodyType2D.Static;
    }


    // Possible shortcomings?
    // collision exit counts anything, including other boxes
    // which will make the box temporarily unmovable 


    // Start is called before the first frame update
    void Start() {
        
        // fetch the rigidbody
        // make the boxes start off not movable
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

    }
}
