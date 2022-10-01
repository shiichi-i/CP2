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
        
        if(this.GetComponent<ObjInfo>().isSpecial && transform.parent.tag != "CodeArea"){
            selectedObj = this.transform.parent.gameObject;
        }else{
            selectedObj = this.gameObject;
        }
        
        avoidCollision.selectedObj = selectedObj;
        mat = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();

    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag != "CodeArea" && other.tag != "Untagged" && outline.moving)
        {
            if(!selectedObj.GetComponent<ObjInfo>().isSpecial){
                avoidCollision.isColliding = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        avoidCollision.isColliding = false;
        if(outline.currentObj == this.gameObject)
        {
            if (this.gameObject.GetComponent<ObjInfo>().isSpecial)
            {
                for(int i = 0; i < this.transform.parent.childCount; i++){
                    this.gameObject.transform.GetChild(i).GetComponent<Renderer>().material = mat.normal;
                }
            }
            else
            {
                if(this.gameObject.GetComponent<ObjInfo>().isPart && !this.gameObject.GetComponent<ObjInfo>().isMicrocontroller){
                    this.gameObject.GetComponent<Renderer>().material = mat.partNorm;
                }else{
                    this.gameObject.GetComponent<Renderer>().material = mat.normal;
                }
                
            }
        }
        
        
        
    }
}
