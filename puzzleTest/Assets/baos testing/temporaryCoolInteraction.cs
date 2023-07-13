using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporaryCoolInteraction : MonoBehaviour
{
    public GameObject thing;
    public GameObject thingTwo;

    // Start is called before the first frame update
    void Start()
    {
        thing.transform.position = new Vector3(500, 500, 0);
        thingTwo.transform.position = new Vector3(500, 500, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e")) {
            thing.transform.position = new Vector3(4.6f, -3.1f, 0.0f);
        }
        if (Input.GetKeyDown("q")) {
            thingTwo.transform.position = new Vector3(2.34f, -3.1f, 0.0f);
        }
    }
}
