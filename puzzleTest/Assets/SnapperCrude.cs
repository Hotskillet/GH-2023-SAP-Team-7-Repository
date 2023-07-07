using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapperCrude : MonoBehaviour
{
    //when the mothership is dropped
    //the goober checks for others
    //if it detects any, it runs a coordinate check
    //coordinates as in the coord id's of the other motherships


    //SETTING UP BLANK LIST OF COLLISIONS I HAVE NO IDEA WHAT IMD OING
    List<Collider2D> colliders = new List<Collider2D>();
    private Collider2D col;
    private ContactFilter2D filter = new ContactFilter2D().NoFilter();


    //relative grid coords
    public int gridX;
    public int gridY;

    //when the mouse goes down then its active
    void OnMouseDown() {

        col = gameObject.GetComponent<BoxCollider2D>();
    }
    
    void OnMouseUp() {
        //HOW DO I GET THE COLLISIONS
        //IM 
        int throwThisNumberAway = Physics2D.OverlapCollider(col, filter, colliders);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
