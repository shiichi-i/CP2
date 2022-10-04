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
                    for(int i = 0; i < 2; i++){
                            if( this.transform.GetChild(i).GetComponent<Rigidbody>() != null){
                                this.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = true;
                                this.transform.GetChild(i).GetComponent<MeshCollider>().isTrigger = true;
                            }
                        }
                }else{
                    this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
                }
                    
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

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
                    for(int i = 0; i < 2; i++){
                            if( this.transform.GetChild(i).GetComponent<Rigidbody>() != null){
                                this.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                                this.transform.GetChild(i).GetComponent<MeshCollider>().isTrigger = false;
                            }
                        }
                }else{
                    this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                }
                    
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

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
