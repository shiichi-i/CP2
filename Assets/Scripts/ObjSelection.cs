using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelection : MonoBehaviour
{
    public GameObject tempObj = null;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Selectable")
                {
                    if (tempObj != hit.collider.gameObject && tempObj != null)
                    {
                        Destroy(tempObj.GetComponent<Outline>());
                    }
                    if(hit.collider.gameObject.GetComponent<Outline>() == null)
                    {
                        hit.collider.gameObject.AddComponent<Outline>();
                    }
                    tempObj = hit.collider.gameObject;   
                }
                else
                {
                    if(tempObj != null)
                    {
                        Destroy(tempObj.GetComponent<Outline>());
                    }
                }
            }
            else
            {
                if (tempObj != null)
                {
                    Destroy(tempObj.GetComponent<Outline>());
                }
            }
        }
    }
}
