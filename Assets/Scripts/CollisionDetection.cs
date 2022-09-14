using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    AvoidCollision avoidCollision;
    public GameObject selectedObj;

    void Start()
    {
        avoidCollision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        selectedObj = this.gameObject;
        avoidCollision.selectedObj = selectedObj;
    }

    void OnTriggerStay(Collider other)
    {
        avoidCollision.isColliding = true;
    }
    void OnTriggerExit(Collider other)
    {
        avoidCollision.isColliding = false;
    }
}
