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

    RewindManager rewinder;

    public AudioSource click;
    public AudioSource plop;

    public GameObject FoundParent;

    public GameObject tutorial;

    void Start(){
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        rewinder = GameObject.Find("SimBar").GetComponentInChildren<RewindManager>();
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
            click.Play();
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
                if(target.name == "pb_microcontroller(Clone)"){
                    GameObject temp = target;
                    target = current;
                    current = temp;
                    select.currentObj = current;
                    select.tempObj = current;
                    Destroy(target.GetComponent<GreenOutline>());
                }
                else if(current.name == "pb_microcontroller(Clone)"){
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

            int indx = 0;
            for(int i = 0; i < rewinder.robotParts.Count; i++){
                if(rewinder.robotParts[i] == target){
                    indx = i;
                }
            }

            rewinder.robotParts.Remove(rewinder.robotParts[indx]);
            indx = 0;

            //SAVE_manager.Instance.RemoveItem(target.GetComponent<ObjInfo>().SaveID);

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

                if(tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 2){
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
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
        plop.Play();
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
        rewinder.robotParts.Add(oldConn);
        //SAVE_manager.Instance.AddItem(oldConn);

        oldConn.GetComponent<ObjInfo>().ParentID = null;

        if(tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 3){
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
                }

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
