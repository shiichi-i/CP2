using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public TransformManager manager;
    public string axis;

    void Start()
    {
        manager = GameObject.Find("SimBar").GetComponent<TransformManager>();
    }

    void OnMouseDown()
    {
        manager.dragAxis = this.gameObject;
        manager.axis = axis;
    }
}
