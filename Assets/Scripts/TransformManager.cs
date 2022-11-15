using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    public GameObject dragAxis = null;
    public string axis;
    Vector3 newPos, newRot;
    public float sensitivity;
    ObjSelection selection;
    public bool overlap;

    public GameObject tutorial;
    bool tutmoves;

    void Start()
    {
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    void Update()
    {
        float cam = Camera.main.transform.parent.transform.eulerAngles.y;
        if (Camera.main.transform.parent.transform.eulerAngles.y > 180f)
            cam = cam / 2f;
        cam = -cam;

        if (Input.GetMouseButton(0))
        {
            if (dragAxis != null)
            {

                selection.moving = true;
                newPos = dragAxis.transform.parent.transform.position;
                Transform ry = null;

                if (axis == "rx" || axis == "ry" || axis == "rz")
                {
                    newRot = selection.currentObj.transform.eulerAngles;
                    ry = dragAxis.transform.parent.transform;
                }
                    tutmoves = true;

                if (axis == "x")
                {
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

                if (axis == "rx")
                {
                    newRot.y -= Input.GetAxis("Mouse X") *  sensitivity * 14f;
                }
                else if (axis == "ry")
                {
                    newRot.z -= Input.GetAxis("Mouse Y") * sensitivity * 14f;
                }
                else if (axis == "rz")
                {
                    newRot.x += Input.GetAxis("Mouse Y") * sensitivity * 14f;
                }


                dragAxis.transform.parent.transform.position = newPos;
                if(axis == "rx" || axis == "ry" || axis == "rz")
                {
                    selection.currentObj.transform.eulerAngles = newRot;
                    ry.eulerAngles = newRot;
                }
                

            }
            
        }
        if(Input.GetMouseButtonUp(0) && tutmoves){
            if(tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 1){
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().NextTut();
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
                }
        }

       if (dragAxis == null)
        {
            overlap = false;
        }


        else if (Input.GetMouseButtonUp(0) && dragAxis != null )
        {
            axis = null;
            dragAxis = null;
            selection.moving = false;
        }
           
    }
}
