using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Items/ItemDatabase", order = 0)]
public class ItemDatabase : ScriptableObject 
{
    public List<ItemData> database = new List<ItemData>();
}
