using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_spawnpoint : MonoBehaviour
{
    VP_manager manager;

    void Start()
    {
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        manager.spawnEmpty = false;
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        manager.spawnEmpty = true;
    }
}
