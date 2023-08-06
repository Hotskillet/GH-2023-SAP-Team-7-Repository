using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockUntouched : MonoBehaviour
{

    private Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            rb.bodyType = RigidbodyType2D.Dynamic;
        } /*else if (collision.gameObject.tag != "Player") {
            rb.bodyType = RigidbodyType2D.Static;

        }
        */
    }

    void OnCollisionExit2D(Collision2D collision) {
            rb.bodyType = RigidbodyType2D.Static;
    }

    // Start is called before the first frame update
    void Start() {

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
