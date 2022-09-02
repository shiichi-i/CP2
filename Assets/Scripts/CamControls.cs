using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamControls : MonoBehaviour
{
    public GameObject pivot;


    Vector3 newPosition, _LocalRotation;

    Vector3 camPos, pivPos;
    Quaternion camRot, pivRot;

    public float panSensitivity = 10f;
    float MouseSensitivity = 2f;
    float OrbitDampening = 10f;

    public bool onCamStart;
    public bool onUIStart;


    bool RotPressed, PanPressed;
    public bool pressed = false;


    void Start()
    {
        pivot = GameObject.Find("CamTarget");
        newPosition = transform.position;

        camPos = transform.position;
        camRot = transform.rotation;
        pivPos = pivot.transform.position;
        pivRot = pivot.transform.rotation;
  
    }
    void LateUpdate()
    {
        if (!IsMouseOverUI())
        {
            HandleZooming();
        }
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (!IsMouseOverUI() && !onUIStart)
            {
                onCamStart = true;
            }
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            if (onCamStart)
            {
                onCamStart = false;
            }
        }
    }

    public void HandlePanning()
    {
        if (Input.GetMouseButton(2) && !RotPressed)
        {
            PanPressed = true;
            pressed = true;

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                newPosition.x -= Input.GetAxis("Mouse X") * panSensitivity;
                newPosition.y -= Input.GetAxis("Mouse Y") * panSensitivity;
                newPosition.z = transform.localPosition.z;
            }
            else
            {
                newPosition.x = transform.localPosition.x;
                newPosition.y = transform.localPosition.y;
                newPosition.z = transform.localPosition.z;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(2))
        {
            newPosition = transform.localPosition;
            PanPressed = false;
            pressed = false;
        }
    }

    public void HandleRotating()
    {

        if (Input.GetMouseButton(1) && !PanPressed)
        {
            RotPressed = true;
            pressed = true;
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                if (_LocalRotation.y < -90f)
                    _LocalRotation.y = -90f;
                else if (_LocalRotation.y > 50f)
                    _LocalRotation.y = 50f;

            }

            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, QT, Time.deltaTime * OrbitDampening);

        }
        if (Input.GetMouseButtonUp(1))
        {
            RotPressed = false;
            pressed = false;
        }


    }

    void HandleZooming()
    {
        if (Input.mouseScrollDelta.y > 0 && !pressed)
        {
            pivot.transform.position += transform.forward;
        }
        if (Input.mouseScrollDelta.y < 0 && !pressed)
        {
            pivot.transform.position -= transform.forward;
        }
    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void ResetCamera()
    {
        pivot.transform.rotation = pivRot;
        pivot.transform.position = pivPos;
        transform.position = camPos;
        transform.rotation = camRot;
    }
}