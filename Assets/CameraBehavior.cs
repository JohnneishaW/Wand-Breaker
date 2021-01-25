using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().aspect = 1920f / 1080;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

