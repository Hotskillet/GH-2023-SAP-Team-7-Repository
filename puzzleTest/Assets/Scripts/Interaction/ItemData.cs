using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    MISC,
    KEY,
    DOOR,
    CONTAINER
};

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData", order = 1)]
public class ItemData : ScriptableObject 
{
    // the type of item (use enum)
    public ItemType iType;
    // bao understands this
    public string comment;
    public Sprite sprite;
    // only for the static object that needs a "key"
    public List<string> canInteractWith = new List<string>();
}
