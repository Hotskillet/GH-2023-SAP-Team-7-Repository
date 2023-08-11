using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JigsawMenu : Singleton<JigsawMenu>
{
    public GameObject jigsawMenu;
    public GameObject gridParent;
    public List<Transform> spawnAreas;
    public System.Random rnd;

    private List<GameObject> unfoundJigsawPieces;
    private List<GameObject> foundJigsawPieces;
    private int spawnCounter;


    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();

        EvtSystem.EventDispatcher.AddListener<LoadPieces>(SavePieces);
        EvtSystem.EventDispatcher.AddListener<FoundAPiece>(UpdateFoundState);
        // Keep an eye out for if the player want's to open/close the menu
        EvtSystem.EventDispatcher.AddListener<TurnOnJigsawMenu>(OpenMenu);
        EvtSystem.EventDispatcher.AddListener<TurnOffJigsawMenu>(CloseMenu);

        unfoundJigsawPieces = new List<GameObject>();
        foundJigsawPieces = new List<GameObject>();
        spawnCounter = 0;
    }

    void OpenMenu (TurnOnJigsawMenu evt){
        jigsawMenu.SetActive(true);
        HideUnfoundPieces();
    }
    void CloseMenu (TurnOffJigsawMenu evt){
        jigsawMenu.SetActive(false);
    }

    void HideUnfoundPieces(){
        foreach (GameObject unfound in unfoundJigsawPieces){
            unfound.SetActive(false);
        }
    }

    void SavePieces(LoadPieces evt){
        // save generated pieces into list of unfound jigsaw pieces
        for (int i = 0; i < evt.gridHeight; i++){
            for (int j = 0; j < evt.gridWidth; j++){
                unfoundJigsawPieces.Add(evt.pieces[i,j]);
            }
        }
    }

    void UpdateFoundState(FoundAPiece evt){
        // pick random piece to display
        int rndIndex = rnd.Next(0, unfoundJigsawPieces.Count);
        GameObject found = unfoundJigsawPieces[rndIndex];
        // set pieces position & parent
        found.transform.position = spawnAreas[spawnCounter].position;
        found.transform.parent = gridParent.transform;
        // remove & add
        foundJigsawPieces.Add(found);
        unfoundJigsawPieces.RemoveAt(rndIndex);
        // update spawn counter
        spawnCounter++;
    }
}
