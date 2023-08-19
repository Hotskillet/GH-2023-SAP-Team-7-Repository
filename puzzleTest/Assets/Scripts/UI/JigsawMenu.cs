using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JigsawMenu : MonoBehaviour
{
    // FIXME: pieces not visible in jigsaw menu

    public GameObject jigsawMenu;
    public GameObject gridParent;
    public List<Transform> spawnAreas;
    public System.Random rnd;

    public List<GameObject> unfoundJigsawPieces;
    public List<GameObject> foundJigsawPieces;
    private int spawnCounter;


    void Awake()
    {
        EvtSystem.EventDispatcher.AddListener<LoadPieces>(SavePieces);
        EvtSystem.EventDispatcher.AddListener<FoundAPiece>(UpdateFoundState);
        // Keep an eye out for if the player want's to open/close the menu
        EvtSystem.EventDispatcher.AddListener<TurnOnJigsawMenu>(OpenMenu);
        EvtSystem.EventDispatcher.AddListener<TurnOffJigsawMenu>(CloseMenu);
        EvtSystem.EventDispatcher.AddListener<ChangeParent>(UpdateMenuParent);

        unfoundJigsawPieces = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jigsawMenu.SetActive(false);
        rnd = new System.Random();

        foundJigsawPieces = new List<GameObject>();
        spawnCounter = 0;
    }

    void OpenMenu (TurnOnJigsawMenu evt){
        jigsawMenu.SetActive(true);
        foreach (GameObject found in foundJigsawPieces){
            found.SetActive(true);
        }
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
        for (int i = 0; i < PieceGenerator.Instance.height; i++){
            for (int j = 0; j < PieceGenerator.Instance.width; j++){
                unfoundJigsawPieces.Add(PieceGenerator.Instance.gridPieces[i,j]);
            }
        }
        HideUnfoundPieces();
    }

    void UpdateFoundState(FoundAPiece evt){
        // if unfound list emmpty, skip
        if (unfoundJigsawPieces.Count == 0){
            print("all pieces found");
            return;
        }
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

    void UpdateMenuParent(ChangeParent evt){
        jigsawMenu.transform.parent = evt.newParent;
        jigsawMenu.transform.position = evt.newParent.position;
    }

    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<LoadPieces>(SavePieces);
        EvtSystem.EventDispatcher.RemoveListener<FoundAPiece>(UpdateFoundState);
        EvtSystem.EventDispatcher.RemoveListener<TurnOnJigsawMenu>(OpenMenu);
        EvtSystem.EventDispatcher.RemoveListener<TurnOffJigsawMenu>(CloseMenu);
        EvtSystem.EventDispatcher.RemoveListener<ChangeParent>(UpdateMenuParent);
    }
}
