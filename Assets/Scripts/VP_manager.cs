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

    bool onSize;

    void Start()
    {
        start = GameObject.Find("Start");
        onSize = true;
    }

    void resize(GameObject looop, bool action, bool start){
         if(onSize && start){
            if(action){
                looop.GetComponentInChildren<VP_loopSize>().extend();
            }else{
                Debug.Log("execute shrink");
                looop.GetComponentInChildren<VP_loopSize>().shrink();
            }
            onSize = false;
         }
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
        
        /*if(dragging != null && colliding != null && colliding.GetComponentInChildren<VP_shadow>().loopParent != null && 
        dragging.GetComponentInChildren<VP_shadow>().loopParent == null && colliding.GetComponentInChildren<VP_loopSize>() == null){
            colliding.GetComponent<VP_shadow>().loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
            resize(colliding.GetComponent<VP_shadow>().loopParent,true, true);
        }

        if(!onShadow && dragging != null && dragging.GetComponentInChildren<VP_loopSize>() == null && dragging.GetComponentInChildren<VP_shadow>().loopParent != null){
            if(colliding == null || !colliding.GetComponent<VP_shadow>().inSide){
                dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
                resize(dragging.GetComponentInChildren<VP_shadow>().loopParent,false, true); 
            }
        }
        */
            
        if (dragging != null && colliding != null && colliding.GetComponent<VP_shadow>().occupied == null && dropped)
        {  
            onSize = true;
            colliding.GetComponent<VP_shadow>().occupied = dragging;
            dragging.GetComponent<VP_drag>().selected = false;

            colliding.GetComponent<Image>().enabled = false;
            dragging.transform.position = colliding.transform.position;

            if(colliding.name == "shad_Loop_in"){
                dragging.transform.SetParent(colliding.transform);
            }else{
                dragging.transform.SetParent(colliding.transform.parent);
            }

            dragging = null;
            colliding = null;
            dropped = false;
        }
        
        if(dragging != null && colliding == null)
        {
            if(dragging.transform.Find("shad_Loop_in") != null){
                dragging.GetComponentInChildren<VP_shadow>().inSide = true;
                dragging.GetComponentInChildren<VP_shadow>().onCallExit = true;
            }else if(dragging.GetComponentInChildren<VP_shadow>() != null && dragging.GetComponentInChildren<VP_shadow>().loopParent != null){
                dragging.GetComponentInChildren<VP_shadow>().onCallExit = true;
            }
            
            dragging.transform.SetParent(start.transform.parent);
            
        }


    }
}
