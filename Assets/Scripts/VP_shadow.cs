using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_shadow : MonoBehaviour
{
    public VP_drag drag;
    public VP_manager manager;
    public bool taken;
    public GameObject occupied, parent;

    void Start()
    {
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
        parent = this.gameObject.transform.parent.gameObject;
        drag = parent.GetComponent<VP_drag>();
    }

    void LateUpdate()
    {
        if (manager.colliding !=null && manager.colliding != this.gameObject)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }
        if(manager.dropped && occupied !=null && !taken)
        {
            occupied = null;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Selectable" && !drag.selected && manager.onShadow)
        {
            taken = true;
            if (occupied == null)
            { 
                this.gameObject.GetComponent<Image>().enabled = true;
                manager.colliding = this.gameObject;
            }
            else if(occupied != null && occupied == manager.dragging)
            {
                occupied = null;
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.tag != "Selectable" || other.gameObject.name != parent.name)
        {
            taken = false;
            this.gameObject.GetComponent<Image>().enabled = false;
            manager.colliding = null;
        }

        if (manager.colliding != this.gameObject)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
