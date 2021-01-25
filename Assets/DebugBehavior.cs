using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehavior : MonoBehaviour
{

    public GameObject enemy;
    public Transform playerTransform;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject newenemy =  Instantiate(enemy, new Vector3(-6, 3, 0), Quaternion.identity);
            newenemy.SendMessage("SetTarget", playerPosition);
            newenemy.SendMessage("SetPlayer", player);
            Debug.Log("debug Spawned Enemy ");
        }*/
    }
}
