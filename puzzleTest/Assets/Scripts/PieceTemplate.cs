using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ConPos{
    public int relX;
    public int relY;
};

[CreateAssetMenu(fileName = "PieceTemplate", menuName = "ScriptableObjects/PieceTemplate", order = 1)]
public class PieceTemplate : ScriptableObject
{
    public Sprite spriteMask;
    // {top, right, bottom, left}
    public int[] sideID = new int[4];
    // positions for connection colliders
    public ConPos connecitonPositionA;
    public ConPos connecitonPositionB;
    public ConPos connecitonPositionC;
    public ConPos connecitonPositionD;
}
