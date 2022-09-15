using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_shadow : MonoBehaviour
{
    public VP_drag drag;
    public VP_manager manager;
    public bool vis;

    void Start()
    {
        drag = this.gameObject.transform.parent.gameObject.GetComponent<VP_drag>();
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
    }

    void LateUpdate()
    {
        if (manager.colliding !=null && manager.colliding != this.gameObject)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            vis = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Selectable" && !drag.selected && manager.onShadow)
        {
            manager.colliding = this.gameObject;
            vis = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Selectable" || other.gameObject.name != this.gameObject.transform.parent.gameObject.name)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            manager.colliding = null;
            vis = false;
        }

        if (manager.colliding != this.gameObject)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            vis = false;
        }
    }
}
