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

            // WHAT????????
            /*
             * This checks the name of the <ItemData> Scriptable Object and 
             * sees if it matches the given name "itemName"
             * Also, don't worry, I had to change this at least ten times bc it 
             * wasn't working or was checking the wrong thing (;_;)
             * Once it finds an <ItemData> with a matching name, it returns it.
             */
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
