using System;
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
    public GameObject connectionPrefab;
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

    // rotates position of connections
    Tuple<ConPos,int> RotateConPos(ConPos pos, float shifts){
        // skip -3
        if (pos.relX == -3){
            return new Tuple<ConPos, int>(pos,0);
        }

        ConPos newConPos;
        int r = (int) Mathf.Sqrt(Mathf.Pow((float) pos.relX, 2.0f) + Mathf.Pow((float) pos.relY, 2.0f));
        
        int theta; // degrees
        if ((pos.relX == 1) || (pos.relX == 2)){
            theta = 0;
        }else if((pos.relY == 1) || (pos.relY == 2)){
            theta = 90;
        }else if((pos.relX == -1) || (pos.relX == -2)){
            theta = 180;
        }else{
            theta = 270;
        }

        int direction = (int) ((theta / 90) + shifts) % 4;
        switch(direction){
            case 1:
                newConPos.relX = 0;
                newConPos.relY = r;
                break;
            case 2:
                newConPos.relX = -r;
                newConPos.relY = 0;
                break;
            case 3:
                newConPos.relX = 0;
                newConPos.relY = -r;
                break;
            default: //0
                newConPos.relX = r;
                newConPos.relY = 0;
                break;
        }

        return new Tuple<ConPos, int>(newConPos, direction);
    }

    //
    Vector3 CalcConnectionDistance(int dir){
        float dist = 0.7f;
        Vector3 spawnPos = new Vector3(0,0,0);
        switch (dir){
            case 1:
                spawnPos.y += dist;
                break;
            case 2:
                spawnPos.x -= dist;
                break;
            case 3:
                spawnPos.y -= dist;
                break;
            default:
                spawnPos.x += dist;
                break;
        }

        return spawnPos;
    }

    // instantiate connections and set as child object to piece
    void CreateConnections(ConPos[] poses, Vector3 newPosition, GameObject go, int[] dirs){
        Vector3 posCon;
        GameObject connection;
        if (poses[0].relX != -3){
            posCon = new Vector3(newPosition.x + (poses[0].relX * 0.2f),
                                newPosition.y + (poses[0].relY * 0.2f),
                                0.0f);
            connection = Instantiate(connectionPrefab, posCon, Quaternion.identity);
            Snap conSnap = connection.GetComponent<Snap>();
            conSnap.connectionDist = CalcConnectionDistance(dirs[0]);
            connection.transform.parent = go.transform;
            conSnap.Init();
        }
        if (poses[1].relX != -3){
            posCon = new Vector3(newPosition.x + (poses[1].relX * 0.2f),
                                newPosition.y + (poses[1].relY * 0.2f),
                                0.0f);
            connection = Instantiate(connectionPrefab, posCon, Quaternion.identity);
            Snap conSnap = connection.GetComponent<Snap>();
            conSnap.connectionDist = CalcConnectionDistance(dirs[1]);
            connection.transform.parent = go.transform;
            conSnap.Init();
        }
        if (poses[2].relX != -3){
            posCon = new Vector3(newPosition.x + (poses[2].relX * 0.2f),
                                newPosition.y + (poses[2].relY * 0.2f),
                                0.0f);
            connection = Instantiate(connectionPrefab, posCon, Quaternion.identity);
            Snap conSnap = connection.GetComponent<Snap>();
            conSnap.connectionDist = CalcConnectionDistance(dirs[2]);
            connection.transform.parent = go.transform;
            conSnap.Init();
        }
        if (poses[3].relX != -3){
            posCon = new Vector3(newPosition.x + (poses[3].relX * 0.2f),
                                newPosition.y + (poses[3].relY * 0.2f),
                                0.0f);
            connection = Instantiate(connectionPrefab, posCon, Quaternion.identity);
            Snap conSnap = connection.GetComponent<Snap>();
            conSnap.connectionDist = CalcConnectionDistance(dirs[3]);
            connection.transform.parent = go.transform;
            conSnap.Init();
        }
    }

    // instantiates pieces with proper connections
    GameObject CreatePiece(PieceTemplate temp, int[] pos, int rot){
        Vector3 newPosition = new Vector3(topLeftCornerPosition.position.x + (pos[0] * dist),
                                                    topLeftCornerPosition.position.y - (pos[1] * dist),
                                                    topLeftCornerPosition.position.z);
        Quaternion newRotation = Quaternion.Euler(0, 0, rot);
        GameObject go = Instantiate(piecePrefab, newPosition, newRotation);
        // rotate side id if needed
        int[] newSideID;
        Tuple<ConPos, int>[] data = new Tuple<ConPos, int>[4];
        ConPos[] newConPoses;
        int[] dirs;
        switch (rot){
            case 90: // left-shift by 1
                newSideID = new int[4] {temp.sideID[1], temp.sideID[2], temp.sideID[3], temp.sideID[0]};
                // add connection colliders
                //FIXME: rotate connection positions
                data[0] = RotateConPos(temp.connecitonPositionA, 1);
                data[1] = RotateConPos(temp.connecitonPositionB, 1);
                data[2] = RotateConPos(temp.connecitonPositionC, 1);
                data[3] = RotateConPos(temp.connecitonPositionD, 1);
                newConPoses = new ConPos[4] {data[0].Item1, data[1].Item1, data[2].Item1, data[3].Item1};
                dirs = new int[4] {data[0].Item2, data[1].Item2, data[2].Item2, data[3].Item2};
                CreateConnections(newConPoses, newPosition, go, dirs);
                break;
            case 180: // left-shift by 2
                newSideID = new int[4] {temp.sideID[2], temp.sideID[3], temp.sideID[0], temp.sideID[1]};
                // add connection colliders
                data[0] = RotateConPos(temp.connecitonPositionA, 2);
                data[1] = RotateConPos(temp.connecitonPositionB, 2);
                data[2] = RotateConPos(temp.connecitonPositionC, 2);
                data[3] = RotateConPos(temp.connecitonPositionD, 2);
                newConPoses = new ConPos[4] {data[0].Item1, data[1].Item1, data[2].Item1, data[3].Item1};
                dirs = new int[4] {data[0].Item2, data[1].Item2, data[2].Item2, data[3].Item2};
                CreateConnections(newConPoses, newPosition, go, dirs);
                break;
            case 270: // left-shift by 3
                newSideID = new int[4] {temp.sideID[3], temp.sideID[0], temp.sideID[1], temp.sideID[2]};
                // add connection colliders
                data[0] = RotateConPos(temp.connecitonPositionA, 3);
                data[1] = RotateConPos(temp.connecitonPositionB, 3);
                data[2] = RotateConPos(temp.connecitonPositionC, 3);
                data[3] = RotateConPos(temp.connecitonPositionD, 3);
                newConPoses = new ConPos[4] {data[0].Item1, data[1].Item1, data[2].Item1, data[3].Item1};
                dirs = new int[4] {data[0].Item2, data[1].Item2, data[2].Item2, data[3].Item2};
                CreateConnections(newConPoses, newPosition, go, dirs);
                break;
            default: // left-shift by 0
                newSideID = temp.sideID;
                // add connection colliders
                data[0] = RotateConPos(temp.connecitonPositionA, 0);
                data[1] = RotateConPos(temp.connecitonPositionB, 0);
                data[2] = RotateConPos(temp.connecitonPositionC, 0);
                data[3] = RotateConPos(temp.connecitonPositionD, 0);
                newConPoses = new ConPos[4] {data[0].Item1, data[1].Item1, data[2].Item1, data[3].Item1};
                dirs = new int[4] {data[0].Item2, data[1].Item2, data[2].Item2, data[3].Item2};
                CreateConnections(newConPoses, newPosition, go, dirs);
                break;
        }
        // save sideID FIXME: not being updated
        go.GetComponent<Piece>().sideID = newSideID;
        // save coordinates
        go.GetComponent<Piece>().coordinates = pos;
        //FIXME: save sprite mask
        go.GetComponent<SpriteRenderer>().sprite = temp.spriteMask;
        

        return go;
    }

    /*
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
