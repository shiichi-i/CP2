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
                if (this.gameObject.GetComponent<ObjInfo>().isSpecial)
                {
                    this.gameObject.GetComponentInChildren<MeshCollider>().isTrigger = true;
                    this.gameObject.GetComponentInChildren<Rigidbody>().isKinematic = true;
                }
                else
                {
                    this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }

                if(this.transform.childCount > 0 && this.transform.GetChild(0).tag == "Player"){
                        for(int i = 0; i < this.transform.childCount; i++){
                            if( this.transform.GetChild(i).GetComponent<Rigidbody>() != null){
                                this.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = true;
                            }
                        }
                    }

            }
            else
            {
                if (this.gameObject.GetComponent<ObjInfo>().isSpecial)
                {
                    this.gameObject.GetComponentInChildren<MeshCollider>().isTrigger = false;
                    this.gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
                }
                else
                {
                    this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                if(this.transform.childCount > 0 && this.transform.GetChild(0).tag == "Player"){
                        for(int i = 0; i < this.transform.childCount; i++){
                            if( this.transform.GetChild(i).GetComponent<Rigidbody>() != null){
                                this.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                            }
                        }
                    }
            }
        }
    }
}
