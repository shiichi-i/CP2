using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_Trash : MonoBehaviour
{
    VP_manager manager;

    void Start()
    {
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
    }

    void OnTriggerStay2D(Collider2D other){
        manager.colliding = this.gameObject;
    }

    void OnTriggerExit2D(Collider2D other){
        manager.colliding = null;
    }
}
