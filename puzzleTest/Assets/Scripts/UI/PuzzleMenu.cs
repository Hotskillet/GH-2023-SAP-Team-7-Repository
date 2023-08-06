using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleMenu : MonoBehaviour
{
    private int gridWidth;
    private int gridHeight;
    private GameObject[,] orderedPieces;
    // this keeps track of when pieces were found
    private bool[,] wasFound;
    private int[] pos; // [x, y]


    // Start is called before the first frame update
    void Start()
    {
        EvtSystem.EventDispatcher.AddListener<LoadPieces>(SavePieces);
        EvtSystem.EventDispatcher.AddListener<FoundAPiece>(UpdateFoundState);

        pos = new int[] {0,0};
    }

    void InitializeFoundStates(int w, int h){
        wasFound = new bool[w, h];
        for (int i = 0; i < w; i++){
            for (int j = 0; j < h; j++){
                wasFound[i,j] = false;
            }
        }
    }

    void SavePieces(LoadPieces evt){
        gridWidth = evt.gridWidth;
        gridHeight = evt.gridHeight;
        orderedPieces = evt.pieces;
        InitializeFoundStates(gridWidth, gridHeight);
    }

    void UpdateFoundState(FoundAPiece evt){
        if ((pos[0] >= gridWidth) && (pos[1] < gridHeight)){
            pos[1]++;
            pos[0] = 0;
        }else{
            pos[0]++;
        }
        // FIXME: add way for pieces to spawn onto menu
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
