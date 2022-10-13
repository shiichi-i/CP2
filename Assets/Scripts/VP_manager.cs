using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_manager : MonoBehaviour
{
    public bool isSelect, onShadow, dropped, spawnEmpty;
    public GameObject dragging = null ,colliding = null;
    public GameObject start;
    public int blocks;

    void Start()
    {
        start = GameObject.Find("Start");
    }

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
            
        if (dragging != null && colliding != null && colliding.GetComponent<VP_shadow>().occupied == null && dropped)
        {  
            colliding.GetComponent<Image>().enabled = false;
            dragging.transform.position = colliding.transform.position;
            colliding.GetComponent<VP_shadow>().occupied = dragging;

            if(colliding.name == "shad_Loop_in"){
                dragging.transform.SetParent(colliding.transform);
                colliding.GetComponent<VP_loopSize>().counted = false;
            }else{
                dragging.transform.SetParent(colliding.transform.parent);

            }


            dragging = null;
            colliding = null;
            dropped = false;
        }
        else if (dragging != null && colliding == null)
        {
            if(dragging.transform.Find("shad_Loop_in") != null){
                dragging.GetComponentInChildren<VP_shadow>().inSide = true;
            }else if(dragging.GetComponentInChildren<VP_shadow>() != null){
                dragging.GetComponentInChildren<VP_shadow>().inSide = false;
            }
            dragging.GetComponentInChildren<VP_shadow>().onCallEnter = true;
            dragging.transform.SetParent(start.transform.parent);
            
        }


    }
}
