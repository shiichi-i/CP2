using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    AvoidCollision avoidCollision;
    public GameObject selectedObj;
    SpawnManager mat;
    ObjSelection outline;
    Color this_color;
    float this_transparency;

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
        
        if(!this.GetComponent<ObjInfo>().isMerged && other.tag != "CodeArea" && other.tag != "Untagged")
        {
            if(other.GetComponent<ObjInfo>() != null){
                if(this.gameObject.GetComponent<Renderer>().material.color != null){
                    this_color = this.gameObject.GetComponent<Renderer>().material.color;
                    this_transparency = this_color.a;
                }
                
                if(this.GetComponent<ObjInfo>().isSpecial){
                    if(other.transform.parent != this.transform.parent && !other.GetComponent<ObjInfo>().connected){
                        avoidCollision.isColliding = true;
                    }
                }else{
                    if(!other.GetComponent<ObjInfo>().isSpecial){
                        avoidCollision.isColliding = true;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.gameObject.GetComponent<ObjInfo>().isSpecial)
        {
            for(int i = 0; i < 2; i++){
                if(selectedObj.transform.GetChild(i).GetComponent<Renderer>() != null){
                    selectedObj.transform.GetChild(i).GetComponent<Renderer>().material = mat.normal;
                }
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

        avoidCollision.isColliding = false;
        if(this.gameObject.GetComponent<ObjInfo>().isPart){
            this.gameObject.GetComponent<Renderer>().material.color = new Color(this_color.r, this_color.g, this_color.b, this_transparency);
        }
    }
}
