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
        if(target != null && target != current){
            Component m = current.AddComponent<FixedJoint>().connectedBody = target.GetComponent<Rigidbody>();
            UnityEditorInternal.ComponentUtility.MoveComponentUp(m);
            Destroy(current.GetComponent<GreenOutline>());
            target.transform.SetParent(current.transform);
            target.tag = "Player";
            if(target.GetComponent<ObjInfo>().isSpecial){
                foreach (Transform child in target.transform){
                    child.tag = "Player";
                }
            }

        GameObject newArrow = Instantiate(addArrow) as GameObject;
        addArrow.transform.localPosition = current.transform.localPosition;
        Transform rot = addArrow.transform.Find("R-Y");
        rot.transform.eulerAngles = current.transform.eulerAngles;
        current.transform.SetParent(newArrow.transform);
        current.GetComponent<ObjInfo>().isMerged = true;

        select.currentObj = current;
        current.AddComponent<Outline>();
        merging = false;
        current = null;
        target = null;
        }
    }

    public void FindParent(){
        while(pChild.transform.parent.tag != "Selectable"){
            pChild = pChild.transform.parent.gameObject;
        }
        select.currentObj = pChild.transform.parent.gameObject;
        pChild = null;
    }

    public void OnUnmerge(){
        
        current = select.currentObj;
        target = current.GetComponent<FixedJoint>().connectedBody.gameObject;

        GameObject arrow = current.transform.parent.gameObject;
        current.transform.SetParent(null);
        Destroy(arrow);
        Destroy(current.GetComponent<Outline>());


        target.transform.SetParent(null);
        target.tag = "Selectable";
            if(target.GetComponent<ObjInfo>().isSpecial){
                foreach (Transform child in target.transform){
                    child.tag = "Selectable";
                }
            }
        Destroy(current.GetComponent<FixedJoint>());

        GameObject newArrow = Instantiate(addArrow) as GameObject;
        addArrow.transform.localPosition = target.transform.localPosition;
        Transform rot = addArrow.transform.Find("R-Y");
        rot.transform.eulerAngles = target.transform.eulerAngles;
        target.transform.SetParent(newArrow.transform);
        target.GetComponent<ObjInfo>().isMerged = false;

        collision.selectedObj = target;
        select.currentObj = target;
        select.tempObj = target;
        target.AddComponent<Outline>();
        merging = false;
        current = null;
        target = null;
    }
}
