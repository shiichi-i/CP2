using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_shadSens : MonoBehaviour
{
    public GameObject occupied;
    public VP_manager manager;
    
    void Start(){
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
    }

    void Update(){
        if(transform.childCount == 0){
            occupied = null;
        }
    }


    void OnTriggerStay2D(Collider2D other){
        if (other.tag == "Sensor" && other.gameObject == manager.dragging)
        {
            if (occupied == null)
            {
                manager.colliding = this.gameObject;
                this.gameObject.GetComponent<Image>().enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        manager.colliding = null;
        this.gameObject.GetComponent<Image>().enabled = false;
    }
    
}
