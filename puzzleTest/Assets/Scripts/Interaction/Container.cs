using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Unlockable
{
    // an item contained in this object will be a child
    public GameObject treasure;

    // Start is called before the first frame update
    void Start()
    {
        try {
            treasure = gameObject.transform.GetChild(0).gameObject;
            treasure.SetActive(false);
        } catch {
            treasure = null;
        }
    }

    private void unlock(){
        // call Unlockable.interact()
        base.interact();
        // check if successful interaction
        if (base.GetFoundKey() && (treasure != null)){
            treasure.SetActive(true);
        }
    }

    // FIXME: remove since Player will be calling these functions
    private void OnCollisionExit2D(Collision2D other) {
        unlock();
    }
}
