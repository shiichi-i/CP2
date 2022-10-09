using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRed : MonoBehaviour
{
    public Material normal, red;
    AvoidCollision collision;
    ObjSelection select;

    void Start()
    {
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }
 
    void Update()
    {
        if(collision.isColliding && select.currentObj == this.gameObject.transform.parent.gameObject)
        {
            this.gameObject.GetComponent<Renderer>().material = red;
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = normal;
        }
    }
}
