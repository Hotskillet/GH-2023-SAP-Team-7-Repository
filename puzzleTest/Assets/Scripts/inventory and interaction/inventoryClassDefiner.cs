using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryClassDefiner : MonoBehaviour {

    //this is the list we will use to reference stuff
    public string[] masterListStrings = new string[] {"put", "item", "names", "here"};

    //this is the same indexed list but with the sprites
    public List<Sprite> masterListSprites = new List<Sprite> {};
    public List<Sprite> masterListUISprites = new List<Sprite> {};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
