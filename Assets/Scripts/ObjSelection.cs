using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelection : MonoBehaviour
{
    public GameObject tempObj = null;
    public GameObject currentObj = null;
    SpawnManager spawn;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !spawn.willSpawn)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Selectable")
                {
                    if (tempObj != hit.collider.gameObject && tempObj != null)
                    {
                        currentObj = null;
                        Destroy(tempObj.GetComponent<Outline>());
                        Destroy(tempObj.GetComponent<CollisionDetection>());
                    }
                    if(hit.collider.gameObject.GetComponent<Outline>() == null)
                    {
                        currentObj = hit.collider.gameObject;
                        currentObj.AddComponent<CollisionDetection>();
                        currentObj.AddComponent<Outline>();
                    }
                    tempObj = hit.collider.gameObject;   
                }
                else
                {
                    if(tempObj != null)
                    {
                        currentObj = null;
                        Destroy(tempObj.GetComponent<Outline>());
                        Destroy(tempObj.GetComponent<CollisionDetection>());
                    }
                }
            }
            else
            {
                if (tempObj != null)
                {
                    currentObj = null;
                    Destroy(tempObj.GetComponent<Outline>());
                }
            }
        }
    }
}
