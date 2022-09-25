using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRed : MonoBehaviour
{
    public Material normal, red;
    AvoidCollision collision;

    void Start()
    {
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
    }
 
    void Update()
    {
        if(collision.isColliding && collision.selectedObj == this.gameObject.transform.parent.gameObject)
        {
            this.gameObject.GetComponent<Renderer>().material = red;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = normal;
        }
    }
}
