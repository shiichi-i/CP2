using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelection : MonoBehaviour
{
    public GameObject tempObj = null;
    public GameObject currentObj = null;
    GameObject unmerge;
    SpawnManager spawn;
    public GameObject arrows;
    public bool moving = false;
    TransformManager arrow;
    AvoidCollision collision;
    InspectorControl inspector;
    omMerge merge;

    public bool play;

    SimManager sim;
    IsMouseOverUI ui;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
        ui = GameObject.Find("SimBar").GetComponent<IsMouseOverUI>();
        arrow = GameObject.Find("SimBar").GetComponent<TransformManager>();
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        merge = GameObject.Find("ShortCuts").GetComponent<omMerge>();
        unmerge = GameObject.Find("UNMR");
    }

    void Update()
    {
            if(currentObj != null && currentObj.GetComponent<ObjInfo>().isMerged){
                unmerge.SetActive(true);
            }else{
                unmerge.SetActive(false);
            }

        if (Input.GetMouseButtonDown(0) && !spawn.willSpawn && !collision.isColliding && !merge.merging)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Selectable" && arrow.dragAxis == null)
                {
                    if (!ui.IsMouseOnUI())
                    {
                        if (tempObj != null && hit.collider.gameObject.transform.parent == null)
                        {
                            if (tempObj != hit.collider.gameObject)
                            {
                                currentObj = null;
                                if (!sim.Playing)
                                {
                                    GameObject arrow = tempObj.transform.parent.gameObject;

                                    tempObj.transform.SetParent(null);
                                    Destroy(arrow);
                                }
                                Destroy(tempObj.GetComponent<Outline>());
                                Destroy(tempObj.GetComponent<CollisionDetection>());
                            }
                        }
                        else if (tempObj != null && hit.collider.gameObject.transform.parent != null)
                        {

                            if (tempObj != hit.collider.gameObject.transform.parent.gameObject)
                            {
                                currentObj = null;
                                if (!sim.Playing)
                                {
                                    GameObject arrow = tempObj.transform.parent.gameObject;
                                    tempObj.transform.SetParent(null);
                                    Destroy(arrow);
                                }
                                Destroy(tempObj.GetComponent<Outline>());
                                Destroy(tempObj.GetComponent<CollisionDetection>());
                            }
                        }


                        if (hit.collider.gameObject.GetComponent<ObjInfo>().isSpecial)
                            {
                                currentObj = hit.collider.gameObject.transform.parent.gameObject;
                            }
                            else
                            {
                                currentObj = hit.collider.gameObject;
                            }

                            tempObj = currentObj;

                            if (currentObj.GetComponent<Outline>() == null)
                            {
                                if (!sim.Playing)
                                {
                                    GameObject arrow = Instantiate(arrows) as GameObject;
                                    arrow.transform.position = currentObj.transform.position;
                                    Transform rot = arrow.transform.Find("R-Y");
                                    rot.transform.eulerAngles = currentObj.transform.eulerAngles;
                                    currentObj.transform.SetParent(arrow.transform);
                                }
                                currentObj.AddComponent<CollisionDetection>();
                                currentObj.AddComponent<Outline>();
                                
                            }
                            

                    }
                }
                else if(hit.collider.tag == "Player" && !ui.IsMouseOnUI()){
                    merge.pChild = hit.collider.gameObject;
                    merge.FindParent();
                    if(tempObj != null && tempObj != currentObj){
                        if (!sim.Playing)
                                {
                                    GameObject arrow = tempObj.transform.parent.gameObject;
                                    tempObj.transform.SetParent(null);
                                    Destroy(arrow);
                                }
                                Destroy(tempObj.GetComponent<Outline>());
                                Destroy(tempObj.GetComponent<CollisionDetection>());
                    }
                    
                    if (currentObj.GetComponent<Outline>() == null)
                    {
                        tempObj = currentObj;
                        if (!sim.Playing)
                        {
                            GameObject arrow = Instantiate(arrows) as GameObject;
                            arrow.transform.position = currentObj.transform.position;
                            Transform rot = arrow.transform.Find("R-Y");
                            rot.transform.eulerAngles = currentObj.transform.eulerAngles;
                            currentObj.transform.SetParent(arrow.transform);
                        }
                        currentObj.AddComponent<CollisionDetection>();
                        currentObj.AddComponent<Outline>();
                    }
                }
                else
                {
                    if (tempObj != null && !moving && !arrow.overlap && !ui.IsMouseOnUI())
                    {
                        currentObj = null;
                        if (!sim.Playing)
                        {
                            GameObject arrow = tempObj.transform.parent.gameObject;
                            tempObj.transform.SetParent(null);
                            Destroy(arrow);
                        }
                        Destroy(tempObj.GetComponent<Outline>());
                        Destroy(tempObj.GetComponent<CollisionDetection>());
                        tempObj = null;
                    }
                }
            }
            else
            {
                if (tempObj != null && !moving && arrow.dragAxis == null && !ui.IsMouseOnUI())
                {
                    currentObj = null;
                    if (!sim.Playing)
                    {
                        GameObject arrow = tempObj.transform.parent.gameObject;

                        tempObj.transform.SetParent(null);
                        Destroy(arrow);
                    }
                    Destroy(tempObj.GetComponent<Outline>());
                    Destroy(tempObj.GetComponent<CollisionDetection>());
                    tempObj = null;
                }
            }
        }
        
        if(merge.merging && Input.GetMouseButtonDown(0)){
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Selectable")
                {
                    if(hit.collider.gameObject.transform.parent == null){
                        merge.target = hit.collider.gameObject;
                    }else{
                        merge.target = hit.collider.gameObject.transform.parent.gameObject;
                    }
                    merge.OnMerge();
                    
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

        if (play)
        {
            currentObj = null;
            if (sim.Playing)
            {
                GameObject arrow = tempObj.transform.parent.gameObject;
                tempObj.transform.SetParent(null);
                Destroy(arrow);
            }
            Destroy(tempObj.GetComponent<Outline>());
            Destroy(tempObj.GetComponent<CollisionDetection>());
            tempObj = null;
            play = false;
            moving = false;
        }
    }
}

