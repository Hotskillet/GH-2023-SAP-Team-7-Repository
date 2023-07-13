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


    // MEL I HAVE NO IDEA HOW THIS WORKS
    // search for and return item
    public ItemData GetData(string itemName){
        foreach (ItemData item in database.database){
            if (item.name.Equals(itemName)){
                return item;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
