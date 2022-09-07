using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeAreaControls : MonoBehaviour
{
    Vector3 newPosition, resetPos, resetZoom;
    public float panSensitivity = 4f;
    bool xLimit, yLimit;

    public RectTransform panel;
    float panelY, panelX;

    public bool onUIStart;
    public bool onCamStart;


    void Start()
    { 
        newPosition = transform.localPosition;
        panel = GameObject.Find("Panel").GetComponent<RectTransform>();
        panelY = panel.localScale.y;
        panelX = panel.localScale.x;

        resetPos = transform.localPosition;
        resetZoom = panel.localScale;
    }

    void LateUpdate()
    {
        if (IsMouseOnUI())
        {
            HandleZooming();
        }
        if (Input.GetMouseButtonDown(2))
        {
            if (IsMouseOnUI() && !onCamStart)
            {
                onUIStart = true;
            }
        }
        if (Input.GetMouseButtonUp(2))
        {
            if (onUIStart)
            {
                onUIStart = false;
            }
        }


    }

    public void HandlePanning()
    {
        if (Input.GetMouseButton(2))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                if (!xLimit)
                {
                    newPosition.x += Input.GetAxis("Mouse X") * panSensitivity;
                }
                if (!yLimit)
                {
                    newPosition.y += Input.GetAxis("Mouse Y") * panSensitivity;
                }
            }
            else
            {
                newPosition = transform.localPosition;
            }

            transform.localPosition = newPosition;
        }
        if (Input.GetMouseButtonUp(2))
        {
            newPosition = transform.localPosition;
        }
    }

    public void HandleLimits()
    {
        if (transform.localPosition.x >= 70)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                xLimit = true;
                newPosition = new Vector3(transform.localPosition.x, newPosition.y, transform.localPosition.z);

            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                xLimit = false;
            }
        }
        else if(transform.localPosition.x <= -70)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                xLimit = false;
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                xLimit = true;
                newPosition = new Vector3(transform.localPosition.x, newPosition.y, transform.localPosition.z);
            }
        }
        else
        {
            xLimit = false;
        }

        if (transform.localPosition.y >= 55)
        {
            if (Input.GetAxis("Mouse Y") > 0)
            {
                yLimit = true;
                newPosition = new Vector3(newPosition.x, transform.localPosition.y, transform.localPosition.z);
            }
            if (Input.GetAxis("Mouse Y") < 0)
            {
                yLimit = false;
            }
        }
        else if (transform.localPosition.y <= -55)
        {
            if (Input.GetAxis("Mouse Y") > 0)
            {
                yLimit = false;
            }
            if (Input.GetAxis("Mouse Y") < 0)
            {
                yLimit = true;
                newPosition = new Vector3(newPosition.x, transform.localPosition.y, transform.localPosition.z);
            }
        }
        else
        {
            yLimit = false;
        }
            
    }

    void HandleZooming()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if(panelX <= 1 && panelY <= 1) { 
                panelX += 0.1f;
                panelY += 0.1f;

                panel.localScale = new Vector3(panelX, panelY, panel.localScale.z);
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (panelX >= 0.3 && panelY >= 0.3)
            {
                panelX -= 0.1f;
                panelY -= 0.1f;

                panel.localScale = new Vector3(panelX, panelY, panel.localScale.z);
            }
        }
    }

    private bool IsMouseOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void Reset()
    {
        transform.localPosition = resetPos;
        panel.localScale = resetZoom;
    }
}
