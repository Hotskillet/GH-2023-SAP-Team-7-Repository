using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceTemplate", menuName = "ScriptableObjects/PieceTemplate", order = 1)]
public class PieceTemplate : ScriptableObject
{
    public GameObject piecePrefab;
    // {top, right, bottom, left}
    public int[] sideID = new int[4];
}
