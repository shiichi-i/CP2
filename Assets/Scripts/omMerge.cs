using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class omMerge : MonoBehaviour
{
    public GameObject current, target;
    public GameObject pChild;
    public bool merging;
    ObjSelection select;
    public GameObject addArrow;
    AvoidCollision collision;
    public FixedJoint[] joints;

    public GameObject FoundParent;

    void Start(){
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
    }
    
    public void OnPickMerge(){
        merging = true;
        current = select.currentObj;

        GameObject arrow = current.transform.parent.gameObject;
        current.transform.SetParent(null);
        Destroy(arrow);
        Destroy(current.GetComponent<Outline>());
        current.AddComponent<GreenOutline>();
    }

    public void OnMerge(){
        if(target != null && target != current && !collision.isColliding){
            
            if(!target.GetComponent<ObjInfo>().isSpecial){
                if(target.transform.childCount > 0 && target.transform.GetChild(0).gameObject.tag == "Player"){
                    GameObject temp = target;
                    target = current;
                    current = temp;
                    select.currentObj = current;
                    select.tempObj = current;
                    Destroy(target.GetComponent<GreenOutline>());
                }
            }else{
                if(target.name == "pb_microcontroller"){
                    GameObject temp = target;
                    target = current;
                    current = temp;
                    select.currentObj = current;
                    select.tempObj = current;
                    Destroy(target.GetComponent<GreenOutline>());
                }
                else if(current.name == "pb_microcontroller"){
                    Destroy(target.GetComponent<GreenOutline>());
                }
                else if(target.transform.childCount > 2 && target.transform.GetChild(2).gameObject.GetComponent<ObjInfo>() != null &&
                 target.transform.GetChild(2).gameObject.GetComponent<ObjInfo>().isPart){
                    GameObject temp = target;
                    target = current;
                    current = temp;
                    select.currentObj = current;
                    select.tempObj = current;
                    Destroy(target.GetComponent<GreenOutline>());
                }
            }

            
            current.AddComponent<FixedJoint>().connectedBody = target.GetComponent<Rigidbody>();
            Destroy(current.GetComponent<GreenOutline>());

            current.GetComponent<ObjInfo>().isMerged = true;
            target.transform.SetParent(current.transform);
                target.tag = "Player";
                if(target.GetComponent<ObjInfo>().isSpecial){
                foreach (Transform child in target.transform){
                    child.tag = "Player";
                }
            }

            select.ArrowAdd();

            if(target.GetComponent<ObjInfo>().isMerged && current.GetComponent<ObjInfo>().isMerged){
                target.GetComponent<ObjInfo>().isMerged = false;
                int startCount;

                if(target.name == "pb_rotational(Clone)"){
                    startCount = 3;
                }else{
                    startCount = 0;
                }

                for(int i = startCount; i < target.transform.childCount; i++){
                    if(target.transform.GetChild(i).GetComponent<ObjInfo>() != null){
                        if(target.transform.GetChild(i).GetComponent<ObjInfo>().isPart ||
                        target.transform.GetChild(i).GetComponent<ObjInfo>().isParent){

                            target.transform.GetChild(i).transform.SetParent(current.transform);
                            
                        }
                    }
                    
                    
                }
            }
                current.GetComponent<ObjInfo>().isMerged = true;

                if(current.GetComponent<ObjInfo>().isSpecial){
                    current.transform.GetChild(0).gameObject.AddComponent<CollisionDetection>();
                    if(current.transform.GetChild(1).gameObject.name != "rod"){
                        current.transform.GetChild(1).gameObject.AddComponent<CollisionDetection>();
                    }
                    
                }else{
                    current.AddComponent<CollisionDetection>();
                }
                
        

        select.currentObj = current;
        select.tempObj = current;
        select.AddCol();
        merging = false;
        current = null;
        target = null;
        }
    }

    public void FindParent(){
        while(pChild.transform.parent.tag != "Selectable"){
            pChild = pChild.transform.parent.gameObject;
        }
        FoundParent = pChild.transform.parent.gameObject;
        pChild = null;
    }

    public void OnUnmerge(){

        current = select.currentObj;
        
        FindJoint();
        GameObject oldConn = current.transform.parent.gameObject;

        current.transform.SetParent(null);
        Destroy(current.GetComponent<GreenOutline>());
        current.tag = "Selectable";
            if(current.GetComponent<ObjInfo>().isSpecial){
                foreach (Transform child in current.transform){
                    child.tag = "Selectable";
                }
            }

        select.ArrowAdd();
        select.checkChild = false;

        if(oldConn.GetComponent<ObjInfo>().isSpecial && oldConn.transform.childCount == 2){
            oldConn.GetComponent<ObjInfo>().isMerged = false;
        }
        else if(!oldConn.GetComponent<ObjInfo>().isSpecial && oldConn.transform.childCount == 0){
            oldConn.GetComponent<ObjInfo>().isMerged = false;
        }

        collision.selectedObj = current;
        select.currentObj = current;
        select.tempObj = current;
        merging = false;
        current = null;
        target = null;
    }

    void FindJoint(){
        joints = current.transform.parent.GetComponents<FixedJoint>();
        foreach(FixedJoint j in joints){
            if(j.connectedBody.gameObject == current){
                target = j.connectedBody.gameObject;
                Destroy(j);
            }
        }
    }
}
