using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // for onscreen inventory
    public Transform slot0;
    public Transform slot1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // call to show an image in the oncreen inventory
    public void ShowInInventory(Sprite img, int spot){
        switch (spot){
            case 0:
                //FIXME: add img to slot0 location
                break;
            case 1:
                //FIXME: add img to slot1 location
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
