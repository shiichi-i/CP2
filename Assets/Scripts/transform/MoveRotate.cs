using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotate : MonoBehaviour
{

    public GameObject arrows;
    public GameObject tempObj = null;
    public Transform target;

    public double moveSpeed = 0.3;
    public float followSpeed = 10;

    public bool isDragging = false;

    Vector3 lastMousePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Selectable")
                {
                    if (tempObj != hit.collider.gameObject && tempObj != null)
                    {
                        Destroy(tempObj.transform.GetChild(0).gameObject);
                    }
                    if (hit.collider.gameObject.transform.parent == null)
                    {
                        tempObj = hit.collider.gameObject;

                        if(tempObj.transform.childCount < 1)
                        {
                            Instantiate(arrows, tempObj.transform);
                            target = tempObj.gameObject.transform.GetChild(0);
                        }
                        
                    }
                } 
            }
            
        }
        if (Input.GetMouseButton(0))
        {
            //for arrows
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            lastMousePos = Input.mousePosition;
            
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.tag == "Move")
                {
                    isDragging = true;
                    //Movement
                    if (hit.collider.gameObject.name == "X" && isDragging)
                    {
                        Vector3 delta = Input.mousePosition - lastMousePos;
                        Vector3 pos = target.transform.position;
                        pos.x += delta.x * (float)moveSpeed;
                        target.transform.position = pos;
                        lastMousePos = Input.mousePosition;
                        tempObj.transform.position = new Vector3(tempObj.transform.position.x + horizontal * followSpeed, pos.y,pos.z);
                    }
                    if (hit.collider.gameObject.name == "Y" && isDragging)
                    {
                        Vector3 delta = Input.mousePosition - lastMousePos;
                        Vector3 pos = target.transform.position;
                        pos.y += delta.y * (float)moveSpeed;
                        target.transform.position = pos;
                        lastMousePos = Input.mousePosition;
                        tempObj.transform.position = new Vector3(pos.x, tempObj.transform.position.y+ vertical * followSpeed, pos.z);
                    }
                    if (hit.collider.gameObject.name == "Z")
                    {
                        Vector3 delta = Input.mousePosition - lastMousePos;
                        Vector3 pos = target.transform.position;
                        pos.z += delta.y * (float)moveSpeed;
                        pos.z += delta.x * (float)moveSpeed;
                        target.transform.position = pos;
                        lastMousePos = Input.mousePosition;
                        tempObj.transform.position = new Vector3(pos.x, pos.y, tempObj.transform.position.z + vertical);
                    }
                }
                else
                {
                    isDragging = false;
                }
            }
        }
        
        
    }

}
