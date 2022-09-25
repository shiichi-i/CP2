using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool willSpawn, ticked;
    public GameObject prefab;

    IsMouseOverUI mouseUI;
    AvoidCollision avoidCollision;

    public Material transparent, normal;
    ObjSelection outline;
    public LayerMask layerMask;

    void Start()
    {
        mouseUI = GameObject.Find("SimBar").GetComponent<IsMouseOverUI>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        avoidCollision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
    }


    void Update()
    {
        if(prefab != null && willSpawn && ticked)
        {
            outline.currentObj = prefab;
            if (!avoidCollision.isColliding)
            {
                if (prefab.GetComponent<ObjInfo>().isSpecial)
                {
                    prefab.GetComponentInChildren<Renderer>().material = transparent;
                }
                else
                {
                    prefab.GetComponent<Renderer>().material = transparent;
                }
            }
            if (!mouseUI.IsMouseOnUI())
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
                    {
                        prefab.transform.position = new Vector3(hit.point.x, 1f, hit.point.z);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && !avoidCollision.isColliding && !mouseUI.IsMouseOnUI())
            {
                if (outline.currentObj.GetComponent<ObjInfo>().isSpecial)
                {
                    prefab.GetComponentInChildren<Renderer>().material = normal;
                    prefab.GetComponentInChildren<MeshCollider>().isTrigger = false;
                    prefab.GetComponent<Rigidbody>().useGravity = true;
                }
                else
                {
                    prefab.GetComponent<Renderer>().material = normal;
                    prefab.GetComponent<MeshCollider>().isTrigger = false;
                    prefab.AddComponent<Rigidbody>();
                }

                willSpawn = false;

                
                prefab.GetComponent<Rigidbody>().isKinematic = true;

                GameObject arrow = Instantiate(outline.arrows) as GameObject;
                arrow.transform.position = outline.currentObj.transform.position;
                outline.currentObj.transform.SetParent(arrow.transform);

                outline.tempObj = prefab;
                prefab = null;
                ticked = false;
                outline.moving = false;
            }
        }

    }
}
