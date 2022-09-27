using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class omMerge : MonoBehaviour
{
    public GameObject current, target;
    public bool merging;
    ObjSelection select;
    public GameObject addArrow;

    void Start(){
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
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
        if(target != null){
            current.AddComponent<FixedJoint>().connectedBody = target.GetComponent<Rigidbody>();
            Destroy(current.GetComponent<GreenOutline>());
            target.transform.SetParent(current.transform);
            target.tag = "Untagged";
            if(target.GetComponent<ObjInfo>().isSpecial){
                foreach (Transform child in target.transform){
                    child.tag = "Untagged";
                }
            }

        GameObject newArrow = Instantiate(addArrow) as GameObject;
        addArrow.transform.position = current.transform.position;
        Transform rot = addArrow.transform.Find("R-Y");
        rot.transform.eulerAngles = current.transform.eulerAngles;
        current.transform.SetParent(newArrow.transform);
        

        select.currentObj = current;
        current.AddComponent<Outline>();
        merging = false;
        current = null;
        target = null;
        }
    }
}
