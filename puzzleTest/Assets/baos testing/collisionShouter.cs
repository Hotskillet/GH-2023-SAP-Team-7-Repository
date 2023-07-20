using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionShouter : MonoBehaviour {

    

    private Collider2D goober;

    // Start is called before the first frame update
    void Start() {
        goober = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    void onMouseDrag() {

    }

}
