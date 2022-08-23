using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    public Camera cam;
    public float speed = 21f;


    Vector3 newPosition, _LocalRotation;
    float startY, currentY, panY;
    float startX, currentX, panX;


    float MouseSensitivity = 4f;
    float OrbitDampening = 10f;
    float ScrollDampening = 6f;

    bool RotPressed, PanPressed, pressed = false;


    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        newPosition = transform.position;
    }
    private void LateUpdate()
    {
        HandlePanning();
        HandleZooming();
        HandleRotating();
        
    }

    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(2))
        {
            startY = Input.mousePosition.y;
            startX = Input.mousePosition.x;
            pressed = true;
            PanPressed = true;
        }
        if (Input.GetMouseButton(2) && !RotPressed)
        {
            currentY = Input.mousePosition.y;
            currentX = Input.mousePosition.x;
            panY = cam.transform.localPosition.y + (startY - currentY);
            panX = cam.transform.localPosition.x + (startX - currentX);

            newPosition = new Vector3(panX, panY, transform.localPosition.z);

            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, newPosition, Time.deltaTime * speed);
        }
        if (Input.GetMouseButtonUp(2))
        {
            pressed = false;
            PanPressed = false;
        }
    }

    void HandleRotating()
    {

        if (Input.GetMouseButton(1) && !PanPressed)
        {
            pressed = true;
            RotPressed = true;
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
            transform.rotation = Quaternion.Lerp(this.transform.rotation, QT, Time.deltaTime * OrbitDampening);
        }
        if (Input.GetMouseButtonUp(1))
        {
            pressed = false;
            RotPressed = false;
        }
    }

    void HandleZooming()
    {
        if (Input.mouseScrollDelta.y > 0 && !pressed)
        {
            transform.position += transform.forward;
        }
        if (Input.mouseScrollDelta.y < 0 && !pressed)
        {
            transform.position -= transform.forward;
        }
    }
}