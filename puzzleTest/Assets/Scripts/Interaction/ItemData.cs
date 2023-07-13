using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData", order = 1)]
public class ItemData : ScriptableObject 
{
    // bao understands this
      
    public Sprite sprite;
    public string comment;
    public List<string> canInteractWith = new List<string>();
}
