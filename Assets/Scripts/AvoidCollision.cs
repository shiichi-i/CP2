using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{
    public bool isColliding = false;
    public GameObject selectedObj;
    public Material red, temp;
    SpawnManager spawn;
    ObjSelection moving;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        moving = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    void Update()
    {
        if (selectedObj != null)
        {
            if (isColliding && moving.moving)
            {
                if (selectedObj.GetComponent<ObjInfo>().isSpecial)
                {
                    selectedObj.GetComponentInChildren<Renderer>().sharedMaterial = red;
                }
                else
                {
                    selectedObj.GetComponent<Renderer>().material = red;
                }

            }
        }
   
    }
}
