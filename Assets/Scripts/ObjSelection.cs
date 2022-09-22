using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelection : MonoBehaviour
{
    public GameObject tempObj = null;
    public GameObject currentObj = null;
    SpawnManager spawn;
    public GameObject arrows;
    public bool moving = false;
    TransformManager arrow;
    AvoidCollision collision;

    SimManager sim;
    IsMouseOverUI ui;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
        ui = GameObject.Find("SimBar").GetComponent<IsMouseOverUI>();
        arrow = GameObject.Find("SimBar").GetComponent<TransformManager>();
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !spawn.willSpawn && !collision.isColliding)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Selectable" && arrow.dragAxis == null)
                {
                    if (!ui.IsMouseOnUI())
                    {
                        if (tempObj != hit.collider.gameObject && tempObj != null)
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
                        if (hit.collider.gameObject.GetComponent<Outline>() == null)
                        {
                            currentObj = hit.collider.gameObject;
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
                        tempObj = hit.collider.gameObject;
                    }
                }
                else
                {
                    if (tempObj != null && !moving && !arrow.overlap)
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
                if (tempObj != null && !moving && arrow.dragAxis == null)
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
    }
}

