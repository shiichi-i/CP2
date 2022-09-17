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

    SimManager sim;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !spawn.willSpawn)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && !moving)
            {
                if(hit.collider.tag == "Selectable")
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
                    if(hit.collider.gameObject.GetComponent<Outline>() == null)
                    {
                        currentObj = hit.collider.gameObject;
                        if (!sim.Playing)
                        {
                            GameObject arrow = Instantiate(arrows) as GameObject;
                            arrow.transform.position = currentObj.transform.position;
                            currentObj.transform.SetParent(arrow.transform);
                        }
                        currentObj.AddComponent<CollisionDetection>();
                        currentObj.AddComponent<Outline>();
                    }
                    tempObj = hit.collider.gameObject;   
                }
                else
                {
                    if(tempObj != null && !moving)
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
                if (tempObj != null && !moving)
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
