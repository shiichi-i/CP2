using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamControls : MonoBehaviour
{
    public Camera cam;
    public float speed = 21f;


    Vector3 newPosition, _LocalRotation;
    public float startY, currentY, panY;
    float startX, currentX, panX;


    float MouseSensitivity = 4f;
    float OrbitDampening = 10f;


    bool RotPressed, PanPressed;
    public bool pressed = false;


    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        newPosition = transform.position;
    }
    void LateUpdate()
    {
        if (!IsMouseOverUI()) { 
            HandlePanning();
            HandleZooming();
            HandleRotating();
        }
        if (Input.GetMouseButtonUp(1))
        {
            pressed = false;
            RotPressed = false;
        }
    }

    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(2))
        {
            startY = Input.mousePosition.y;
            startX = Input.mousePosition.x;
            PanPressed = true;
            pressed = true;
        }
        if (Input.GetMouseButton(2) && !RotPressed)
        {
            currentY = Input.mousePosition.y;
            currentX = Input.mousePosition.x;

            panY = cam.transform.localPosition.y + (startY - currentY);
            panX = cam.transform.localPosition.x + (startX - currentX);

            newPosition = new Vector3(panX, panY, 0);
            
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
            transform.rotation = Quaternion.Lerp(this.transform.rotation, QT, Time.deltaTime * OrbitDampening);
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

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}