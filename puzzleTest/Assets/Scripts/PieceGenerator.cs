using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* CONCERNS
[ ] What is the width and height of a single piece? This should be consistent for everyone
    (Artists, Programmers) to make layering the image on top of the piece masks easier.
*/


public class PieceGenerator : Singleton<PieceGenerator>
{
    public GameObject piecePrefab;
    public int width;
    public int height;
    public Transform topLeftCornerPosition;
    public float dist;

    public PieceDatabase database;

    public System.Random rnd;

    private GameObject[,] gridPieces;
    private List<PieceTemplate> cornerPieces;
    private List<PieceTemplate> sidePieces;
    private List<PieceTemplate> middlePieces;

    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        gridPieces = new GameObject[width,height];
        cornerPieces = new List<PieceTemplate>();
        sidePieces = new List<PieceTemplate>();
        middlePieces = new List<PieceTemplate>();

        SortPieces();
        GeneratePieces();
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

    // invert side type: 0->1, 1->0
    int invertSideType(int n){
        if (n == 0){
            return 1;
        }
        return 0;
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
                if (template.sideID[i] != req[i]){
                    skipTemplate = true;
                    break;
                }
            }
            // add template if met requirements
            if (skipTemplate){
                skipTemplate = false;
                continue;
            }
            compatible.Add(template);
        }
        return compatible;
    }

    GameObject CreatePiece(PieceTemplate temp, int[] pos, int rot){
        Vector3 newPosition = new Vector3(topLeftCornerPosition.position.x + (pos[0] * dist),
                                                    topLeftCornerPosition.position.y - (pos[1] * dist),
                                                    topLeftCornerPosition.position.z);
        Quaternion newRotation = Quaternion.Euler(0, 0, rot);
        GameObject go = Instantiate(piecePrefab, newPosition, newRotation);
        // rotate side id if needed
        int[] newSideID;
        switch (rot){
            case 90: // left-shift by 1
                newSideID = new int[4] {temp.sideID[1], temp.sideID[2], temp.sideID[3], temp.sideID[0]};
                break;
            case 180: // left-shift by 2
                newSideID = new int[4] {temp.sideID[2], temp.sideID[3], temp.sideID[0], temp.sideID[1]};
                break;
            case 270: // left-shift by 3
                newSideID = new int[4] {temp.sideID[3], temp.sideID[0], temp.sideID[1], temp.sideID[2]};
                break;
            default:
                newSideID = temp.sideID;
                break;
        }
        // save sideID FIXME: not being updated
        go.GetComponent<Piece>().sideID = newSideID;
        Debug.Log("newSideID:" + newSideID[0] + newSideID[1] + newSideID[2] + newSideID[3]);
        // save coordinates
        go.GetComponent<Piece>().coordinates = pos;
        // save sprite mask
        //go.GetComponent<SpriteMask>().sprite = temp.spriteMask;
        go.GetComponent<SpriteRenderer>().sprite = temp.spriteMask;

        return go;
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
    void GeneratePieces(){
        bool stop = false;
        int rndIndex;
        for (int j = 0; j < height; j++){ // loop through rows
            for (int i = 0; i < width; i++){ // loop through columns
                // top-left (the first piece)
                if ((i == j) && (i == 0)){
                    // choose a corner piece from temporary list of corner pieces
                    rndIndex = rnd.Next(0, cornerPieces.Count);
                    // create piece
                    PieceTemplate tempTemplate = cornerPieces[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 0);
                }
                // middle
                else if ((0 < i) && (i < width-1) && (0 < j) &&(j < height-1)){
                    // determine connection requirements
                    int topID = invertSideType(gridPieces[i, j-1].GetComponent<Piece>().sideID[2]);
                    int leftID = invertSideType(gridPieces[i-1, j].GetComponent<Piece>().sideID[1]);
                    int[] sideReqs = {topID, -1, -1, leftID};
                    // find compatible middle pieces
                    List<PieceTemplate> temp = FindCompatible(middlePieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 0);
                }
                // middle-left
                else if ((i == 0) && (0 < j) && (j < height-1)){
                    // determine connection requirements
                    int topID = invertSideType(gridPieces[i, j-1].GetComponent<Piece>().sideID[2]);
                    int[] sideReqs = {-1, topID, -1, -1};
                    // find compatible side pieces
                    List<PieceTemplate> temp = FindCompatible(sidePieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 90);
                }
                //FIXME: middle-right
                else if ((i == width-1) && (0 < j) && (j < height-1)){
                    // determine connection requirements
                    int topID = invertSideType(gridPieces[i, j-1].GetComponent<Piece>().sideID[2]);
                    int leftID = invertSideType(gridPieces[i-1, j].GetComponent<Piece>().sideID[1]);
                    int[] sideReqs = {-1, -1, leftID, topID};
                    // find compatible side pieces
                    List<PieceTemplate> temp = FindCompatible(sidePieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 270);
                }
                //FIXME: top-middle
                else if ((0 < i) && (i < width-1) && (j == 0)){
                    // determine connection requirements
                    int leftID = invertSideType(gridPieces[i-1, j].GetComponent<Piece>().sideID[1]);
                    int[] sideReqs = {-1, -1, -1, leftID};
                    // find compatible side pieces
                    List<PieceTemplate> temp = FindCompatible(sidePieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 0);
                }
                //FIXME: bottom-middle
                else if ((0 < i) && (i < width-1) && (j == height-1)){
                    // determine connection requirements
                    int topID = invertSideType(gridPieces[i, j-1].GetComponent<Piece>().sideID[2]);
                    int leftID = invertSideType(gridPieces[i-1, j].GetComponent<Piece>().sideID[1]);
                    int[] sideReqs = {-1, leftID, topID, -1};
                    // find compatible side pieces
                    List<PieceTemplate> temp = FindCompatible(sidePieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 180);
                }
                //FIXME: top-right
                else if ((i == width-1) && (j == 0)){
                    // determine connection requirements
                    int leftID = invertSideType(gridPieces[i-1, j].GetComponent<Piece>().sideID[1]);
                    int[] sideReqs = {-1, -1, leftID, -1};
                    Debug.Log("sideReqs: " + sideReqs[0] + sideReqs[1] + sideReqs[2] + sideReqs[3]);
                    // find compatible corner pieces
                    List<PieceTemplate> temp = FindCompatible(cornerPieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 270);
                }
                //FIXME: bottom-left
                else if ((i == 0) && (j == height-1)){
                    // determine connection requirements
                    int topID = invertSideType(gridPieces[i, j-1].GetComponent<Piece>().sideID[2]);
                    int[] sideReqs = {-1, topID, -1, -1};
                    Debug.Log("SideID: " + sideReqs[0] + sideReqs[1] + sideReqs[2] + sideReqs[3]);
                    // find compatible corner pieces
                    List<PieceTemplate> temp = FindCompatible(cornerPieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 90);
                }
                //FIXME: bottom-right
                else if ((i == width-1) && (j == height-1)){
                    // determine connection requirements
                    int topID = invertSideType(gridPieces[i, j-1].GetComponent<Piece>().sideID[2]);
                    int leftID = invertSideType(gridPieces[i-1, j].GetComponent<Piece>().sideID[1]);
                    int[] sideReqs = {-1, leftID, topID, -1};
                    Debug.Log("SideID: " + sideReqs[0] + sideReqs[1] + sideReqs[2] + sideReqs[3]);
                    // find compatible corner pieces
                    List<PieceTemplate> temp = FindCompatible(cornerPieces, sideReqs);
                    if (temp.Count == 0){
                        Debug.Log("NULL at: [" + i + ", " + j + "]");
                        stop = true;
                        break;
                    }
                    // randomly pick one
                    rndIndex = rnd.Next(0, temp.Count);
                    // create piece
                    PieceTemplate tempTemplate = temp[rndIndex];
                    // save piece
                    int[] pos = {i, j};
                    gridPieces[i,j] = CreatePiece(tempTemplate, pos, 180);
                }
                // else null
                else{
                    Debug.Log("NULL at: [" + i + ", " + j + "]");
                }
            }
            if (stop){
                break;
            }
        }
        return;
    }
}
