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

        if(this.GetComponent<ObjInfo>().isSpecial && this.transform.parent.tag != "CodeArea"){      
            if(!this.GetComponent<ObjInfo>().isParent){          
                selectedObj = this.transform.parent.gameObject;
            }else{
                selectedObj = this.gameObject;
            }
        }else{
            selectedObj = this.gameObject;
        }

        avoidCollision.selectedObj = selectedObj;
        mat = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<ObjInfo>() != null){
            if(this.gameObject.GetComponent<ObjInfo>().isPart){
                this_color = this.gameObject.GetComponent<Renderer>().material.color;
                this_transparency = this_color.a;
            }
        }

        if(other.tag != "CodeArea" && other.tag != "Untagged")
        {
            if(this.GetComponent<ObjInfo>().isSpecial){
                if(other.transform.parent != null && other.transform.parent != this.transform.parent && !other.GetComponent<ObjInfo>().connected){
                    avoidCollision.isColliding = true;
                }
            }else{
                avoidCollision.isColliding = true;
            }
        }else if(this.tag == "Player" && !this.GetComponent<ObjInfo>().isMerged){
           if(other.GetComponent<ObjInfo>() != null){
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
        if(other.tag != "CodeArea"){
            if(outline.currentObj != null && outline.currentObj.GetComponent<ObjInfo>().isMerged){
                for(int i = 0; i < outline.currentObj.transform.childCount; i++){
                    if(outline.currentObj.transform.GetChild(i).GetComponent<Renderer>() != null){
                        if(outline.currentObj.transform.GetChild(i).GetComponent<ObjInfo>().isPart && !outline.currentObj.transform.GetChild(i).GetComponent<ObjInfo>().isMicrocontroller){
                            outline.currentObj.transform.GetChild(i).GetComponent<Renderer>().material = mat.partNorm;                  
                        }else{
                            outline.currentObj.transform.GetChild(i).GetComponent<Renderer>().material = mat.normal;
                        }
                        outline.currentObj.transform.GetChild(i).GetComponent<ObjInfo>().SetColor();
                    }
                }
            }
            else if (this.gameObject.GetComponent<ObjInfo>().isSpecial)
            {
                for(int i = 0; i < selectedObj.transform.childCount; i++){
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
            this.GetComponent<ObjInfo>().SetColor();
        }
    }
}
