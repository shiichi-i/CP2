using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindRotMot : MonoBehaviour
{
    public Button find;
    AssignmentControl assign;
    ObjSelection select;
    public GameObject parentMot;
    public Material greeen;
    omMerge merge;

    AvoidCollision coll;

    SpawnManager normMat;

    bool ticked = true;

    void Start(){
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        normMat = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        merge = GameObject.Find("ShortCuts").GetComponent<omMerge>();
        coll = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
    }

    void Update(){
        if(select.currentObj != null){
            if(select.checkChild || select.currentObj.GetComponent<ObjInfo>().isMerged){
                find.interactable = false;
            }else{
                find.interactable = true;
            }
        }

    
    }

    public void OnFindMotor(){
            if(ticked){
            select.currentObj.AddComponent<GreenOutline>();
            
            GameObject arrow = select.currentObj.transform.parent.gameObject;
            select.currentObj.transform.SetParent(null);
            Destroy(arrow);

            for(int i = 0; i < assign.motors.Length; i++){
                if(assign.motors[i] != null && assign.motors[i].name == "o_rotational(Clone)" &&
                !assign.motors[i].GetComponent<ObjInfo>().connected){
                    assign.motors[i].transform.GetChild(0).GetComponent<Renderer>().material = greeen;
                    assign.motors[i].transform.GetChild(1).GetComponent<Renderer>().material = greeen;
                }
            }
            select.onFindMotor = true;
            select.showOnce = true;
        }else{
            Destroy(select.currentObj.GetComponent<GreenOutline>());
            if(select.currentObj.transform.parent == null){
                select.ArrowAdd();
            }
            

            for(int i = 0; i < assign.motors.Length; i++){
                if(assign.motors[i] != null && assign.motors[i].name == "o_rotational(Clone)"){
                    assign.motors[i].transform.GetChild(0).GetComponent<Renderer>().material = normMat.normal;
                    assign.motors[i].transform.GetChild(1).GetComponent<Renderer>().material = normMat.normal;
                }

            }

            select.onFindMotor = false;
        }
        
        
    }

    public void Cancel(){
        Destroy(select.currentObj.GetComponent<GreenOutline>());
        Destroy(select.currentObj.GetComponent<Outline>());
        Destroy(select.currentObj.GetComponent<CollisionDetection>());

        for(int i = 0; i < assign.motors.Length; i++){
                if(assign.motors[i] != null && assign.motors[i].name == "o_rotational(Clone)"){
                    assign.motors[i].transform.GetChild(0).GetComponent<Renderer>().material = normMat.normal;
                    assign.motors[i].transform.GetChild(1).GetComponent<Renderer>().material = normMat.normal;
                }
        }
        select.currentObj = null;
        select.tempObj = null;
        coll.isColliding = false;
    }

    public void SetTransform(){
        Destroy(select.w);
        Destroy(select.currentObj.GetComponent<GreenOutline>());
        Destroy(select.currentObj.GetComponent<Outline>());
        Destroy(select.currentObj.GetComponent<CollisionDetection>());

        for(int i = 0; i < assign.motors.Length; i++){
                if(assign.motors[i] != null && assign.motors[i].name == "o_rotational(Clone)"){
                    assign.motors[i].transform.GetChild(0).GetComponent<Renderer>().material = normMat.normal;
                    assign.motors[i].transform.GetChild(1).GetComponent<Renderer>().material = normMat.normal;
                }
        }

        parentMot.transform.GetChild(0).tag = "Player";
        parentMot.transform.GetChild(1).tag = "Player";
        parentMot.GetComponent<ObjInfo>().connected = true;

        parentMot.transform.GetChild(0).GetComponent<ObjInfo>().connected = true;
        parentMot.transform.GetChild(1).GetComponent<ObjInfo>().connected = true;

        select.currentObj.tag = "Player";
        select.currentObj.GetComponent<ObjInfo>().connection = parentMot;
        select.currentObj.GetComponent<ObjInfo>().connected = true;

        select.currentObj.transform.SetParent(parentMot.transform.GetChild(1));
        select.currentObj.transform.position = parentMot.transform.GetChild(1).GetChild(0).position;
        select.currentObj.transform.rotation = parentMot.transform.rotation;  
        
        select.currentObj.AddComponent<ConfigurableJoint>().connectedBody = parentMot.transform.GetChild(1).GetComponent<Rigidbody>();
        select.currentObj.GetComponent<ConfigurableJoint>().anchor = new Vector3(0,0,0);
        select.currentObj.GetComponent<ConfigurableJoint>().axis = new Vector3(0,0,0);
        select.currentObj.GetComponent<ConfigurableJoint>().secondaryAxis = new Vector3(0,0,0);
        
        select.currentObj.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
        select.currentObj.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
        select.currentObj.GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
        select.currentObj.GetComponent<ConfigurableJoint>().angularXMotion = ConfigurableJointMotion.Free;
        select.currentObj.GetComponent<ConfigurableJoint>().angularYMotion = ConfigurableJointMotion.Locked;
        select.currentObj.GetComponent<ConfigurableJoint>().angularZMotion = ConfigurableJointMotion.Locked;

        select.currentObj.tag = "Player";
        merge.pChild = select.currentObj;
        merge.FindParent();
        select.currentObj = null;
        select.tempObj = null;
        coll.isColliding = false;

        select.onFindMotor = false;

    }
}
