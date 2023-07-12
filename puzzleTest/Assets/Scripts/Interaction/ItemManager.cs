using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public ItemDatabase database;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public ItemData GetData(string itemName){
        return database.TryGetValue(itemName, out item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
