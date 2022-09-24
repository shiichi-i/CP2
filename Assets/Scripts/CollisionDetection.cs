using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    AvoidCollision avoidCollision;
    public GameObject selectedObj;
    SpawnManager mat;

    void Start()
    {
        avoidCollision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        selectedObj = this.gameObject;
        avoidCollision.selectedObj = selectedObj;
        mat = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "CodeArea")
        {
            avoidCollision.isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        avoidCollision.isColliding = false;
        this.gameObject.GetComponent<Renderer>().material = mat.normal;
        
    }
}
