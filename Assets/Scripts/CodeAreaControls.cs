using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeAreaControls : MonoBehaviour
{
    public float speed = 0.4f;
    Vector3 newPosition;
    float startY, currentY, panY;
    float startX, currentX, panX;
    bool xLimit, yLimit;

    public RectTransform panel;
    float panelY, panelX;

    
    void Start()
    {
        newPosition = transform.localPosition;
        panel = GameObject.Find("Panel").GetComponent<RectTransform>();
        panelY = panel.localScale.y;
        panelX = panel.localScale.x;
    }

    void LateUpdate()
    {
        if (IsMouseOnUI())
        {
            HandleZooming();
            HandlePanning();
            HandleLimits();
        }
    }

    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(2))
        {
            startY = -Input.mousePosition.y;
            startX = -Input.mousePosition.x;
        }
        if (Input.GetMouseButton(2))
        {
            currentY = -Input.mousePosition.y;
            currentX = -Input.mousePosition.x;

            panX = transform.localPosition.x + (startX - currentX);
            panY = transform.localPosition.y + (startY - currentY);

            if (!xLimit && !yLimit)
            { 
                newPosition = new Vector3(panX, panY, transform.localPosition.z);
            }
            else if(xLimit && yLimit)
            {
                newPosition = transform.localPosition;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * speed);


        }

    }

    void HandleLimits()
    {
        if (transform.localPosition.x >= 77)
        {
            if (panX > 0)
            {
                xLimit = true;
                newPosition = new Vector3(transform.localPosition.x, panY, transform.localPosition.z);

            }
            if (panX < 0)
            {
                xLimit = false;
            }
        }
        else if(transform.localPosition.x <= -77)
        {
            if (panX > 0)
            {
                xLimit = false;
            }
            if (panX < 0)
            {
                xLimit = true;
                newPosition = new Vector3(transform.localPosition.x, panY, transform.localPosition.z);
            }
        }
        else
        {
            xLimit = false;
        }

        if (transform.localPosition.y >= 55)
        {
            if (panY > 0)
            {
                yLimit = true;
                newPosition = new Vector3(panX, transform.localPosition.y, transform.localPosition.z);
            }
            if (panY < 0)
            {
                yLimit = false;
            }
        }
        else if (transform.localPosition.y <= -55)
        {
            if (panY > 0)
            {
                yLimit = false;
            }
            if (panY < 0)
            {
                yLimit = true;
                newPosition = new Vector3(panX, transform.localPosition.y, transform.localPosition.z);
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
}
