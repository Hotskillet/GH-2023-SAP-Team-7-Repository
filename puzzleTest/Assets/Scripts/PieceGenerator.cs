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
    public int width;
    public int height;
    public Vector3 topLeftCornerPosition;

    public PieceDatabase database;

    public System.Random rnd;

    private PieceTemplate[,] gridPieces;
    private List<PieceTemplate> cornerPieces;
    private List<PieceTemplate> sidePieces;
    private List<PieceTemplate> middlePieces;

    // Start is called before the first frame update
    void Start()
    {
        topLeftCornerPosition = new Vector3();
        rnd = new System.Random();
        gridPieces = new PieceTemplate[width,height];
        cornerPieces = new List<PieceTemplate>();
        sidePieces = new List<PieceTemplate>();
        middlePieces = new List<PieceTemplate>();

        SortPieces();
    }

    // checks if a piece template is a corner piece (top-left corner ONLY)
    bool isCorner(PieceTemplate piece){
        if ((piece.sideID[0] == piece.sideID[3]) && (piece.sideID[3] == 2)){
            return true;
        }
        return false;
    }
    // check if a piece template is a side piece (top side ONLY)
    bool isSide(PieceTemplate piece){
        if ((piece.sideID[0] == 2) && (piece.sideID[3] != 2)){
            return true;
        }
        return false;
    }
    // check if a piece template is a middle piece (any)
    bool isMiddle(PieceTemplate piece){
        if ((piece.sideID[0] != 2) && (piece.sideID[3] != 2)){
            return true;
        }
        return false;
    }

    // sort pieces
    void SortPieces(){
        foreach (PieceTemplate template in database.Data){
            if (isCorner(template)){
                cornerPieces.Add(template);
            }else if (isSide(template)){
                sidePieces.Add(template);
            }else{
                middlePieces.Add(template);
            }
        }
    }

    // searches for peices with a given list that fit the sideID requirements
    List<PieceTemplate> FindCompatible(List<PieceTemplate> source, int[] req){
        bool skipTemplate = false;
        List<PieceTemplate> compatible = new List<PieceTemplate>();
        foreach (PieceTemplate template in source){
            for (int i = 0; i < req.Length; i++){
                // -1 means no requirement
                if (req[i] == -1){
                    continue;
                }
                // check if mismatch
                if (req[i] != template.sideID[i]){
                    skipTemplate = true;
                    break;
                }
            }
            // add template if met requirements
            if (!skipTemplate){
                compatible.Add(template);
            }else{
                // reset flag
                skipTemplate = false;
            }
        }
        return compatible;
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
    void ChoosePieces(){
        int rndIndex;
        for (int j = 0; j < height; j++){ // loop through columns
            for (int i = 0; i < width; i++){ // loop through rows
                // top-left (the first piece)
                if ((i == j) && (i == 0)){
                    // choose a corner piece from temporary list of corner pieces
                    rndIndex = rnd.Next(0, cornerPieces.Count+1);
                    // save as top-left corner piece
                    gridPieces[0,0] = cornerPieces[rndIndex];
                }
                //FIXME: middle
                else if ((0 < i) && (i < width-1) && (0 < j) &&(j < height-1)){
                    // determine connection requirements
                    int topID = -1 * (gridPieces[i, j-1].sideID[2] - 1); // inverts ID (0->1, 1->0)
                    int leftID = -1 * (gridPieces[i-1, j].sideID[1] - 1); // inverts ID (0->1, 1->0)
                    int[] middleReqs = {topID, -1, -1, leftID};
                    // find compatible middle pieces
                    List<PieceTemplate> temp = FindCompatible(middlePieces, middleReqs);
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count+1);
                    gridPieces[i,j] = temp[rndIndex];
                }
                //FIXME: middle-left
                else if ((i == 0) && (0 < j) && (j < height-1)){
                    // determine connection requirements
                    int topID = -1 * (gridPieces[i, j-1].sideID[2] - 1); // inverts ID (0->1, 1->0)
                    int[] sideReqs = {topID, -1, -1, -1};
                    // find compatible side pieces
                    List<PieceTemplate> temp = FindCompatible(sidePieces, sideReqs);
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count+1);
                    gridPieces[i,j] = temp[rndIndex];
                }
                //FIXME: middle-right
                //FIXME: top-middle
                //FIXME: bottom-middle
                //FIXME: top-right
                //FIXME: bottom-left
                //FIXME: bottom-right
            }
        }
        return;
    }

    void GeneratePieces(){
        //FIXME: instantiate prefab of corner piece
        //Instantiate(temp[rndIndex].piecePrefab, topLeftCornerPosition, Quaternion.identity);
    }

    /* FIXME
    Add puzzle picture on top of pieces matrix.
    */
    void Update(){
    }
}
