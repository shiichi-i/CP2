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
    public GameObject tempColliding;
    
    public AudioSource click;

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
           
            dragging.GetComponentInChildren<Image>().color = new Color(1,1,1,1);

            if(dragging.GetComponentInChildren<VP_loopSize>() == null && 
                colliding.GetComponent<VP_loopSize>() == null &&
                dragging.GetComponentInChildren<VP_ifSize>() == null && 
                colliding.GetComponent<VP_ifSize>() == null){
                colliding.GetComponent<Image>().enabled = true;
            }
            
            tempColliding = colliding;
        }
        
        
        if(dragging != null && colliding != null && colliding.name == "Trash" &&
        dragging.gameObject == colliding.GetComponent<VP_Trash>().outhit)
        {
            
            dragging.GetComponentInChildren<Image>().color = new Color(1,0,0,1);
            
            if(dropped)
                Destroy(dragging);

        }
        else if(dragging != null && dragging.tag == "Sensor" && colliding != null && colliding.GetComponent<VP_shadow>() == null &&
        colliding.GetComponent<VP_shadSens>() != null && colliding.GetComponent<VP_shadSens>().occupied == null ){
            
            if(dropped){
                click.Play();
                dragging.transform.position = colliding.transform.position;
                colliding.GetComponent<VP_shadSens>().occupied = dragging;
                dragging.transform.SetParent(colliding.transform);

                dragging = null;
                colliding = null;
                dropped = false;
            }
            

        }else if (dragging != null && colliding != null && colliding.name != "Trash" && colliding.GetComponent<VP_shadow>().occupied == null && dropped)    
        {  
            click.Play();
            
            dragging.GetComponentInChildren<Image>().color = new Color(1,1,1,1);

            colliding.GetComponent<VP_shadow>().occupied = dragging;
            dragging.GetComponent<VP_drag>().selected = false;

            colliding.GetComponent<Image>().enabled = false;
            dragging.transform.position = colliding.transform.position;

            if(colliding.name == "shad_Loop_in"){
                dragging.transform.SetParent(colliding.transform);
                dragging.GetComponentInChildren<VP_shadow>().isLoopParent = true;
            }else if(colliding.name == "shad_ifT" || colliding.name == "shad_ifF"){
                dragging.transform.SetParent(colliding.transform);
                dragging.GetComponentInChildren<VP_shadow>().isLoopParent = false;
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

            }else if(dragging.transform.Find("shad_ifT") != null || dragging.transform.Find("shad_ifF") != null ){
                dragging.GetComponentInChildren<VP_shadow>().inSide = true;
                dragging.GetComponentInChildren<VP_shadow>().onCallExit = true;
                
            }else if(dragging.GetComponentInChildren<VP_shadow>() != null && dragging.GetComponentInChildren<VP_shadow>().loopParent != null){
                dragging.GetComponentInChildren<VP_shadow>().onCallExit = true;
            }
            
            dragging.GetComponentInChildren<Image>().color = new Color(1,1,1,1);

            dragging.transform.SetParent(start.transform.parent);
            
        }


    }
}
