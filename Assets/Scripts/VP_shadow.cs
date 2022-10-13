using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_shadow : MonoBehaviour
{
    public VP_drag drag;
    public VP_manager manager;
    public bool taken, inSide;
    public GameObject occupied, parent;
    float childC;
    public GameObject loopParent;

    public bool onEnter, onExit, onCallEnter, onCallExit;

    bool forLoop;

    void Start()
    {
        childC = transform.parent.childCount;
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
        parent = this.gameObject.transform.parent.gameObject;
        if(this.name == "shad_Loop"){
            drag = parent.transform.parent.GetComponent<VP_drag>();
        }else if(this.name == "shad_Loop_in"){
            drag = parent.GetComponent<VP_drag>();
            inSide = true;
        }else{
            drag = parent.GetComponent<VP_drag>();
        }
        onCallEnter = true;
    }

    void LateUpdate()
    {
        if (manager.colliding !=null && manager.colliding != this.gameObject)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }

        if(this.GetComponent<VP_loopSize>() != null){
            if(transform.childCount == 0){
                occupied = null;
            }
        }else if(transform.parent.childCount == childC){
            occupied = null;
        }
    }

    /*public GameObject findLoopP(){
        bool foundLoop = false;
        Transform loop = this.transform;        
            while(!foundLoop){
                if(loop.GetComponentInChildren<VP_loopSize>() != null){
                     foundLoop = true;
                }else{
                   loop = loop.transform.parent;
                }
            }
        return loop.gameObject;
    }*/

    

    void OnTriggerStay2D(Collider2D other)
    {
        
        if(onCallEnter && other.gameObject == manager.dragging){
            onEnter = true;
            onExit = false;
            onCallExit = true;
            onCallEnter = false;

            //if(inSide && occupied == null)
                //findLoopP().GetComponent<VP_loopSize>().counted = false;
        }

        
        if(other.GetComponentInChildren<VP_loopSize>() != null){
            taken = true;
        }
        if (other.tag != "Selectable" && other.gameObject == manager.dragging && !drag.selected && manager.onShadow)
        //&& other.GetComponent<VP_drag>()!= null && other.GetComponent<VP_drag>().selected)
        {
            taken = true;
            if (occupied == null)
            {
                this.gameObject.GetComponent<Image>().enabled = true;
                manager.colliding = this.gameObject;

                if(inSide && onEnter){
                    if(manager.dragging.GetComponentInChildren<VP_shadow>().inSide){
                        manager.dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
                        manager.dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_loopSize>().shrink();
                    }else{
                        if(this.name == "shad_Loop_in" && occupied == null){
                            GetComponent<VP_loopSize>().counted =false;
                            GetComponent<VP_loopSize>().extend();
                        }else if(loopParent !=null && occupied == null){
                            loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
                            loopParent.GetComponentInChildren<VP_loopSize>().extend();
                        }
                    }
                    onEnter = false;
                }
            }
            else{
                manager.colliding = null;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(onCallExit && other.gameObject == manager.dragging){
            onEnter = false;
            onExit = true;
            onCallEnter = true;
            onCallExit = false;
        }

        if (other.tag != "Selectable" || other.gameObject.name != parent.name)
        {
            taken = false;
            if(inSide && onExit){
                if (this.name == "shad_Loop_in" && occupied == null){
                    GetComponent<VP_loopSize>().counted = false;
                    GetComponent<VP_loopSize>().shrink();
                }else if(loopParent !=null && occupied == null && other.GetComponent<VP_drag>() != null && other.GetComponent<VP_drag>().selected){
                    loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
                    loopParent.GetComponentInChildren<VP_loopSize>().shrink();
                }

                onExit = false;
            }

            this.gameObject.GetComponent<Image>().enabled = false;
            manager.colliding = null; 
        }

        if (manager.colliding != this.gameObject)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
