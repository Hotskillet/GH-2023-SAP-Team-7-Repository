using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* CONCERNS
[ ] What is the width and height of a single piece? This should be consistent for everyone
    (Artists, Programmers) to make layering the image on top of the piece masks easier.
*/


//FIXME: make a singleton
public class PieceGenerator : MonoBehaviour
{
    //FIXME: should this be a scriptable object with a 2D array of the piece templates?
    public Sprite pieceTemplates;
    public int width;
    public int height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /* FIXME
    generate grid of pieces (using width and height variables) by:
        1) Randomly selecting a top-left corner to start with
        2) Checking which kind of pieces can connect to this corner piece,
            then randomly picking among those options.
        3) Repeat for the entire row.
        4) In rows after the 1st, check for a proper top-side connection as well.
        5) Repeat until finished.
        6) save results in matrix
    */

    /* FIXME
    Add puzzle picture on top of pieces matrix.
    */
}
