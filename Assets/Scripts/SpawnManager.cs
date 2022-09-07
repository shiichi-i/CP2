using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool willSpawn = false;
    public GameObject prefab;
    Vector3 newPos;

    IsMouseOverUI mouseUI;

    public Material transparent, normal;
    ObjSelection outline;

    void Start()
    {
        mouseUI = GameObject.Find("SimBar").GetComponent<IsMouseOverUI>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(prefab != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                willSpawn = false;
                prefab.GetComponent<Renderer>().material = normal;
                outline.tempObj = prefab;
                outline.currentObj = null;
                prefab = null;
            }

        }
        
        if (willSpawn)
        {
            outline.currentObj = prefab;
            prefab.GetComponent<Renderer>().material = transparent;
            if (!mouseUI.IsMouseOnUI())
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        newPos = new Vector3(hit.point.x, 1f, hit.point.z);
                    }  
                }
                prefab.transform.position = Vector3.Lerp(prefab.transform.position, newPos, Time.deltaTime * 25f);
            }
        }
    }
}
