using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public TransformManager manager;
    public string axis;
    IsMouseOverUI ui;
    
    void Start()
    {
        manager = GameObject.Find("SimBar").GetComponent<TransformManager>();
        ui = GameObject.Find("SimBar").GetComponent<IsMouseOverUI>();
    }

    void OnMouseDown()
    {
        if (!ui.IsMouseOnUI())
        {
            manager.dragAxis = this.gameObject;
            manager.axis = axis;
            manager.overlap = true;
        }

    }
}
