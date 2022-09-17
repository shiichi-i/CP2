using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    public GameObject dragAxis = null;
    public string axis;
    Vector3 newPos;
    public float sensitivity;
    ObjSelection selection;

    void Start()
    {
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    void Update()
    {
        if (dragAxis != null)
        {
            selection.moving = true;
            newPos = dragAxis.transform.parent.transform.position;
            if (Input.GetMouseButton(0))
            {
                if(axis == "x")
                {
                    float cam = Camera.main.transform.parent.transform.eulerAngles.y;
                    if (Camera.main.transform.parent.transform.eulerAngles.y > 180f)
                        cam = cam / 2f;
                        cam = -cam;
                    
                    if (cam > -90f && cam < 90f)
                     {
                         newPos.x += Input.GetAxis("Mouse X") * sensitivity;
                     }

                    else if (cam > -180f && cam < -145f)
                    {
                        newPos.x += Input.GetAxis("Mouse X") * sensitivity;
                    }

                    else
                     {
                         newPos.x -= Input.GetAxis("Mouse X") * sensitivity;
                     }
                }
                else if (axis == "y")
                {
                    newPos.y += Input.GetAxis("Mouse Y") * sensitivity;
                }
                else if (axis == "z")
                {
                    if (Camera.main.transform.parent.transform.eulerAngles.y > 0 &&
                 Camera.main.transform.parent.transform.eulerAngles.y < 180f)
                        newPos.z -= Input.GetAxis("Mouse X") * sensitivity;

                    else
                        newPos.z += Input.GetAxis("Mouse X") * sensitivity;
                }

                dragAxis.transform.parent.transform.position = newPos;
            }
            if (Input.GetMouseButtonUp(0))
            {
                axis = null;
                dragAxis = null;
                selection.moving = false;
            }
        }
    }
}
