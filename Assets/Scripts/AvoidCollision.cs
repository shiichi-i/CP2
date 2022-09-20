using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{
    public bool isColliding = false;
    public GameObject selectedObj;
    public Material red, temp;
    SpawnManager spawn;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if(selectedObj != null)
        {
            temp = selectedObj.GetComponent<Renderer>().material;
            if (isColliding)
            {
                selectedObj.GetComponent<Renderer>().material = red;
            }
            if (!isColliding)
            {
                selectedObj.GetComponent<Renderer>().material = temp;
            }
        }  
    }
}
