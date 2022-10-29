using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_Trash : MonoBehaviour
{
    VP_manager manager;
    public GameObject outhit;

    void Start()
    {
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
    }

    void OnTriggerStay2D(Collider2D other){
        manager.colliding = this.gameObject;
        outhit = other.gameObject.transform.parent.gameObject;
    }

    void OnTriggerExit2D(Collider2D other){
        manager.colliding = null;
        outhit = null;
    }
}
