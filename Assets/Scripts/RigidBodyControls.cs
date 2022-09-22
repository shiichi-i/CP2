using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyControls : MonoBehaviour
{
    SimManager simulation;
    ObjSelection select;
    public string objType;

    void Start()
    {
        simulation = GameObject.Find("SimBar").GetComponent<SimManager>();
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    void Update()
    {
        if(select.currentObj != this.gameObject)
        {
            if (!simulation.Playing)
            {
                this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
