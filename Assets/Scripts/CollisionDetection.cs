using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    AvoidCollision avoidCollision;
    public GameObject selectedObj;
    SpawnManager mat;
    ObjSelection outline;

    void Start()
    {
        avoidCollision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        selectedObj = this.gameObject;
        avoidCollision.selectedObj = selectedObj;
        mat = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "CodeArea" && outline.moving)
        {
            avoidCollision.isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        avoidCollision.isColliding = false;

        if(outline.currentObj == this.gameObject)
        {
            if (this.gameObject.GetComponent<ObjInfo>().isSpecial)
            {
                this.gameObject.GetComponentInChildren<Renderer>().sharedMaterial = mat.normal;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material = mat.normal;
            }
        }
        
        
        
    }
}
