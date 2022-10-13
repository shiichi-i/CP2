using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_drag : MonoBehaviour
{
    Vector3 newPos;
    public bool selected = false;
    public bool isStart;
    VP_manager manager;

    void Start()
    {
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();

    }

    public void Dragging()
    {
        if (!isStart)
        {
            manager.dropped = false;
            manager.isSelect = true;
            manager.dragging = this.gameObject;
            selected = true;
            newPos.x = Input.mousePosition.x;
            newPos.y = Input.mousePosition.y;
            newPos.z = 0;

            transform.position = newPos;
        }
        
    }

    public void Dropped()
    {
        if (!isStart)
        {
            manager.isSelect = false;
            selected = false;
            manager.dropped = true;
        }
        if(manager.colliding == null){
            manager.dragging = null; 
        }
        manager.spawnEmpty = true;
    }
}
