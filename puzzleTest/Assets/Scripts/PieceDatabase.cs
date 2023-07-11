using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceDatabase", menuName = "ScriptableObjects/PieceDatabase", order = 1)]
public class PieceDatabase : ScriptableObject
{
    public PieceTemplate[] Data;
}
