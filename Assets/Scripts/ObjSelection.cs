using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelection : MonoBehaviour
{
    public GameObject wheelPref, w;
    public GameObject tempObj = null;
    public GameObject currentObj = null;
    GameObject b_unmerge, b_merge;
    SpawnManager spawn;
    public GameObject arrows;
    public bool moving = false;
    TransformManager arrow;
    AvoidCollision collision;
    InspectorControl inspector;
    omMerge merge;

    public bool checkChild;
    public bool onFindMotor;

    public bool play, showOnce;

    SimManager sim;
    IsMouseOverUI ui;
    FindRotMot findRot;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
        ui = GameObject.Find("SimBar").GetComponent<IsMouseOverUI>();
        arrow = GameObject.Find("SimBar").GetComponent<TransformManager>();
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        merge = GameObject.Find("ShortCuts").GetComponent<omMerge>();
        b_unmerge = GameObject.Find("UNMR");
        b_merge = GameObject.Find("MERG");
        findRot = GameObject.Find("Inspector").GetComponent<FindRotMot>();
    }

    void Update()
    {
            if(currentObj != null && checkChild && currentObj.name != "pb_wheel(Clone)"){
                b_unmerge.SetActive(true);
            }else{
                b_unmerge.SetActive(false);
            }

            if(currentObj != null && !checkChild && !collision.isColliding && currentObj.name != "pb_wheel(Clone)"){
                b_merge.SetActive(true);
            }else{
                b_merge.SetActive(false);
            }

        if (Input.GetMouseButtonDown(0) && !spawn.willSpawn && !merge.merging && !onFindMotor)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Selectable" && arrow.dragAxis == null)
                {
                    if (!ui.IsMouseOnUI() && !checkChild)
                    {
                        if (tempObj != null && hit.collider.gameObject.transform.parent == null)
                        {
                            if (tempObj != hit.collider.gameObject)
                            {
                                currentObj = null;
                                if (!sim.Playing && tempObj.transform.parent != null)
                                {
                                    GameObject arrow = tempObj.transform.parent.gameObject;
                                    tempObj.transform.SetParent(null);
                                    Destroy(arrow);
                                }
                                Destroy(tempObj.GetComponent<Outline>());
                                Destroy(tempObj.GetComponent<GreenOutline>());
                                DeleteCol(tempObj);
                            }
                        }
                        else if (tempObj != null && hit.collider.gameObject.transform.parent != null)
                        {

                            if (tempObj != hit.collider.gameObject.transform.parent.gameObject)
                            {
                                currentObj = null;
                                if (!sim.Playing && tempObj.transform.parent.gameObject != null)
                                {
                                    GameObject arrow = tempObj.transform.parent.gameObject;
                                    tempObj.transform.SetParent(null);
                                    Destroy(arrow);
                                }
                                Destroy(tempObj.GetComponent<Outline>());
                                Destroy(tempObj.GetComponent<GreenOutline>());
                                DeleteCol(tempObj);
                            }
                        }


                        if (hit.collider.gameObject.GetComponent<ObjInfo>().isSpecial)
                            {
                                currentObj = hit.collider.gameObject.GetComponent<ObjInfo>().special;
                            }
                            else
                            {
                                currentObj = hit.collider.gameObject;
                            }

                            tempObj = currentObj;
                            

                            if (currentObj.GetComponent<Outline>() == null && !checkChild)
                            {
                                ArrowAdd();
                                AddCol();   
                            }
                            

                    }
                }
                else if(hit.collider.tag == "Player" && !ui.IsMouseOnUI() && !checkChild && arrow.dragAxis == null && !onFindMotor){
                    merge.pChild = hit.collider.gameObject;
                    merge.FindParent();
                    currentObj = merge.FoundParent;
                    if(tempObj != null && tempObj != currentObj){
                        if (!sim.Playing && tempObj.transform.parent != null)
                                {
                                    GameObject arrow = tempObj.transform.parent.gameObject;
                                    tempObj.transform.SetParent(null);
                                    Destroy(arrow);
                                }
                                Destroy(tempObj.GetComponent<Outline>());
                                Destroy(tempObj.GetComponent<GreenOutline>());
                                DeleteCol(tempObj);
                    }
                    
                    if (currentObj.GetComponent<Outline>() == null && !checkChild)
                    {
                        tempObj = currentObj;
                        ArrowAdd();
                        AddCol();
                        
                    }else{
                        if(!checkChild  && arrow.dragAxis == null){
                            checkChild = true;
                            if (!sim.Playing && tempObj.transform.parent != null)
                            {
                                GameObject arrow = tempObj.transform.parent.gameObject;
                                tempObj.transform.SetParent(null);
                                Destroy(arrow);
                            }
                            Destroy(tempObj.GetComponent<Outline>());
                            DeleteCol(tempObj);

                            if(hit.collider.gameObject.GetComponent<ObjInfo>().special != null){
                                currentObj = hit.collider.gameObject.GetComponent<ObjInfo>().special;
                            }else{
                                currentObj = hit.collider.gameObject;
                            }
                                tempObj = currentObj;
                                currentObj.AddComponent<GreenOutline>();
                            
                        }
                
                    }
                }
                else
                {
                    if (tempObj != null && !moving && !arrow.overlap && !ui.IsMouseOnUI())
                    {
                        currentObj = null;
                        if (!sim.Playing && !checkChild && tempObj.transform.parent != null)
                        {
                            GameObject arrow = tempObj.transform.parent.gameObject;
                            tempObj.transform.SetParent(null);
                            Destroy(arrow);
                        }
                        Destroy(tempObj.GetComponent<Outline>());
                        Destroy(tempObj.GetComponent<GreenOutline>());
                        DeleteCol(tempObj);
                        tempObj = null;
                        checkChild = false;
                    }
                }
            }
            else
            {
                if (tempObj != null && !moving && arrow.dragAxis == null && !ui.IsMouseOnUI()  && !checkChild && !onFindMotor)
                {
                    currentObj = null;
                    if (!sim.Playing && tempObj.transform.parent.gameObject != null)
                    {
                        GameObject arrow = tempObj.transform.parent.gameObject;

                        tempObj.transform.SetParent(null);
                        Destroy(arrow);
                    }
                    Destroy(tempObj.GetComponent<Outline>());
                    Destroy(tempObj.GetComponent<GreenOutline>());
                    DeleteCol(tempObj);
                    tempObj = null;
                    checkChild = false;
                }
            }
        }
        
        if(merge.merging && Input.GetMouseButtonDown(0)){
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Selectable" && !hit.collider.gameObject.GetComponent<ObjInfo>().isMerged)
                {
                    if(hit.collider.gameObject.transform.parent == null){
                        merge.target = hit.collider.gameObject;

                    }else{
                        merge.target = hit.collider.gameObject.transform.parent.gameObject;
                    }
                    merge.OnMerge();
                    
                }
                else if(hit.collider.tag != "CodeArea"){
                    if(hit.collider.tag == "Player" || hit.collider.gameObject.GetComponent<ObjInfo>().isMerged){
                        if(!hit.collider.gameObject.GetComponent<ObjInfo>().isMerged){
                            merge.pChild = hit.collider.gameObject;
                            merge.FindParent();
                            merge.target = merge.FoundParent;
                        }else{
                            merge.target = hit.collider.gameObject;
                        }
                            merge.OnMerge();
                    }
                }
            }
        }else if(merge.merging && Input.GetMouseButtonDown(1)){
            Destroy(currentObj.GetComponent<GreenOutline>());
            tempObj = null;
            currentObj = null;
            merge.merging = false;
            merge.current = null;
            merge.target = null;
        }
        
        if(onFindMotor){
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name == "rod" || hit.collider.gameObject.name == "rotational"){
                    if(!hit.collider.GetComponent<ObjInfo>().connected){
                        if(showOnce){
                            Ghost(hit.collider.transform.parent.GetChild(1).GetChild(0));
                        }
                        
                        if(Input.GetMouseButtonDown(0) && !collision.ghostCol){
                            findRot.parentMot = hit.collider.transform.parent.gameObject;
                            findRot.SetTransform();
                        }
                    }
                }else{
                    if(w != null){
                        Destroy(w);
                        showOnce = true;
                        w = null;
                    }
                }
            }else{
                if(w != null){
                    Destroy(w);
                    showOnce = true;
                    w = null;
                }
            }

            if(Input.GetMouseButtonDown(1)){
                findRot.Cancel();
                onFindMotor = false;
            }

        }

        if (play && currentObj != null)
        {
            if (sim.Playing && currentObj.transform.parent.gameObject != null && currentObj.transform.parent.tag == "CodeArea" && !checkChild)
            {
                GameObject arrow = currentObj.transform.parent.gameObject;
                currentObj.transform.SetParent(null);
                Destroy(arrow);
            }
            Destroy(currentObj.GetComponent<Outline>());
            Destroy(currentObj.GetComponent<GreenOutline>());
            DeleteCol(currentObj);
            currentObj = null;
            tempObj = null;
            moving = false;
            if(checkChild){
                checkChild = false;
            }
            play = false;
        }

        
    }

    void Ghost(Transform perent){
        GameObject u = Instantiate(wheelPref) as GameObject;
        u.transform.position = perent.transform.position;
        u.transform.localScale = currentObj.transform.localScale;
        u.transform.parent = perent;
        u.transform.rotation = perent.transform.parent.transform.parent.rotation;
        showOnce = false;
        w = u;
    }

    public void ArrowAdd(){
         if (!sim.Playing)
        {
            GameObject arrow = Instantiate(arrows) as GameObject;
            arrow.transform.position = currentObj.transform.position;
            Transform rot = arrow.transform.Find("R-Y");
            rot.transform.eulerAngles = currentObj.transform.eulerAngles;
            currentObj.transform.SetParent(arrow.transform);
        }
        currentObj.AddComponent<Outline>();
        
    }

    void DeleteCol(GameObject obj){
        if(obj.GetComponent<ObjInfo>().isSpecial){
            Destroy(obj.transform.GetChild(0).gameObject.GetComponent<CollisionDetection>());
        }else{
            Destroy(obj.GetComponent<CollisionDetection>());

        }
    }

    public void AddCol(){
        if(currentObj.GetComponent<ObjInfo>().isMerged){
            for(int i = 0; i < currentObj.transform.childCount; i++){
                if(currentObj.transform.GetChild(i).GetComponent<CollisionDetection>() == null &&
                    currentObj.transform.GetChild(i).name != "CountRot" &&
                    !currentObj.transform.GetComponent<ObjInfo>().isParent){
                     currentObj.transform.GetChild(i).gameObject.AddComponent<CollisionDetection>();
                }
            }
        }
        else if(currentObj.GetComponent<ObjInfo>().isSpecial){
            currentObj.transform.GetChild(0).gameObject.AddComponent<CollisionDetection>();
        }else{
            currentObj.AddComponent<CollisionDetection>();
        }
    }
}

