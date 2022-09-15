using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_manager : MonoBehaviour
{
    public bool isSelect, onShadow, dropped;
    public GameObject dragging = null ,colliding = null;

    void Update()
    {
        if (isSelect && Input.GetMouseButton(0))
        {
            onShadow = true;
        }
        else
        {
            onShadow = false;
        }

        if (colliding != null && onShadow)
        {
            colliding.GetComponent<Image>().enabled = true;
        }
        if (dragging != null && colliding != null && dropped)
        {
            colliding.GetComponent<Image>().enabled = false;
            dragging.transform.position = colliding.transform.position;
            dragging = null;
            colliding = null;
            dropped = false;
        }


    }
}
