using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_drag : MonoBehaviour
{
    Vector3 newPos;
    public bool selected = false;
    public bool isStart;
    VP_manager manager;
    public string BParentID;
    public string vpID;
    public string BlockType;

    public string Sin;

    void Start()
    {
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
    }

    void Update(){
        if(transform.parent != null && transform.parent.tag == "Player"){
            BParentID = transform.parent.GetComponent<VP_drag>().vpID;
        }else if(transform.parent != null && transform.parent.tag == "Sensor"){
            BParentID = transform.parent.parent.GetComponent<VP_drag>().vpID;
        }else if(transform.parent.name == "shad_Loop_in"){
            BParentID = transform.parent.parent.GetComponent<VP_drag>().vpID;
            Sin = "Sloop";
        }else{
            BParentID = null;
            Sin = null;
        }
    }

    public void Dragging()
    {
        if (!isStart)
        {
            if(Input.GetMouseButton(0)){
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
