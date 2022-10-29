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
    public bool isLoopParent;

    public bool onEnter, onExit, onCallEnter, onCallExit;

    bool forLoop;

    void Start()
    {
        childC = transform.parent.childCount;
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
        parent = this.gameObject.transform.parent.gameObject;
        if(this.name == "shad_Loop" || this.name == "shad_if"){
            drag = parent.transform.parent.GetComponent<VP_drag>();
        }else if(this.name == "shad_Loop_in" || this.name == "shad_ifT" || this.name == "shad_ifF"){
            drag = parent.GetComponent<VP_drag>();
            inSide = true;
        }else{
            drag = parent.GetComponent<VP_drag>();
        }
        onCallEnter = true;
    }

    void Update()
    {
        if (manager.colliding !=null && manager.colliding != this.gameObject)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }

        if(this.GetComponent<VP_loopSize>() != null || this.GetComponent<VP_ifSize>() != null ){
            if(transform.childCount == 0){
                occupied = null;
            }
        }else if(transform.parent.childCount == childC){
            occupied = null;
        }else if(this.name == "shad_Loop_in" && transform.childCount == 0){
            occupied = null;
        }else if(transform.childCount == 0){
            if(this.name == "shad_ifT" || this.name == "shad_ifF"){
                occupied = null;
            }
        }

        if(occupied != null || manager.dragging == this.parent){
            GetComponent<BoxCollider2D>().isTrigger = false;
        }else{
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        taken = true;

        if(onCallEnter && other.gameObject == manager.dragging){
            onEnter = true;
            onExit = false;
            onCallExit = true;
            onCallEnter = false;
        }
        if (other.tag != "Selectable" && other.gameObject == manager.dragging && !drag.selected && manager.onShadow
        && other.GetComponent<VP_drag>()!= null && other.GetComponent<VP_drag>().selected)
        {
            
            if (occupied == null && other.tag != "Sensor")
            {
                if(transform.parent.GetComponentInChildren<VP_loopSize>() == null && 
                manager.dragging.GetComponentInChildren<VP_loopSize>() == null ){
                    manager.colliding = this.gameObject;
                    this.gameObject.GetComponent<Image>().enabled = true;
                }else if(transform.parent.GetComponentInChildren<VP_ifSize>() == null && 
                manager.dragging.GetComponentInChildren<VP_ifSize>() == null){
                    manager.colliding = this.gameObject;
                    this.gameObject.GetComponent<Image>().enabled = true;
                }else{
                    manager.colliding = null;
                    this.gameObject.GetComponent<Image>().enabled = false;
                }
                


                if(inSide && onEnter && other.tag != "Sensor"){

                    if(this.name == "shad_Loop_in" && occupied == null &&
                    other.GetComponentInChildren<VP_shadow>().loopParent == null){
                        GetComponent<VP_loopSize>().counted =false;
                        GetComponent<VP_loopSize>().extend();

                    }else if(loopParent !=null && this.name != "shad_Loop_in" && 
                    other.GetComponentInChildren<VP_shadow>().loopParent == null
                    && loopParent.GetComponentInChildren<VP_loopSize>() != null &&
                    this.name != "shad_ifT" && this.name != "shad_ifF"){
                            loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
                            loopParent.GetComponentInChildren<VP_loopSize>().extend();
                            
                    }else{
                        if(occupied == null &&
                        other.GetComponentInChildren<VP_shadow>().loopParent == null){
                            if(this.name == "shad_ifT" || this.name == "shad_ifF"){
                                GetComponent<VP_ifSize>().counted =false;
                                GetComponent<VP_ifSize>().extend();
                            }else{
                                loopParent.GetComponentInChildren<VP_ifSize>().counted = false;
                                loopParent.GetComponentInChildren<VP_ifSize>().extend();
                            }
                        }else if(loopParent !=null &&
                        other.GetComponentInChildren<VP_shadow>().loopParent == null && 
                        loopParent.GetComponentInChildren<VP_ifSize>() != null){
                            loopParent.GetComponentInChildren<VP_ifSize>().counted = false;
                            loopParent.GetComponentInChildren<VP_ifSize>().extend();
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

        taken = false;
        
        if(onCallExit && other.gameObject == manager.dragging){
            onEnter = false;
            onExit = true;
            onCallEnter = true;
            onCallExit = false;
        }

        if (other.tag != "Selectable" || other.gameObject.name != parent.name)
        {
            
            if(inSide && onExit && other.tag != "Sensor"){
                if (this.name == "shad_Loop_in" && occupied == null){
                    GetComponent<VP_loopSize>().counted = false;
                    GetComponent<VP_loopSize>().shrink();
                }else if(this.name == "shad_Loop_in" && occupied != null &&
                manager.colliding == other){
                    if(this.name != "shad_ifT" && this.name != "shad_ifF"){
                        GetComponent<VP_loopSize>().counted = false;
                        GetComponent<VP_loopSize>().shrink();
                    }

                }else if(loopParent !=null && occupied == null && other.GetComponent<VP_drag>() != null && other.GetComponent<VP_drag>().selected &&
                other.GetComponentInChildren<VP_shadow>().loopParent == null && loopParent.GetComponentInChildren<VP_loopSize>() != null && loopParent.GetComponent<IfScript>() == null){
                        loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
                        loopParent.GetComponentInChildren<VP_loopSize>().shrink();
                }else{

                    if(this.name == "shad_ifT" || this.name == "shad_ifF"){
                        if(occupied == null){
                            GetComponent<VP_ifSize>().counted = false;
                            GetComponent<VP_ifSize>().shrink();
                        }else if(occupied != null && manager.colliding == other){
                            GetComponent<VP_ifSize>().counted = false;
                            GetComponent<VP_ifSize>().shrink();
                        }
                    }else if(loopParent !=null && occupied == null && other.GetComponent<VP_drag>() != null && other.GetComponent<VP_drag>().selected &&
                    other.GetComponentInChildren<VP_shadow>().loopParent == null){
                        loopParent.GetComponentInChildren<VP_ifSize>().counted = false;
                        loopParent.GetComponentInChildren<VP_ifSize>().shrink();
                    }
                }

                if(manager.dragging.GetComponentInChildren<VP_shadow>()!= null &&  
                manager.dragging.GetComponentInChildren<VP_shadow>().loopParent == loopParent
                && occupied == null && this.name != "shad_Loop_in" && other != occupied &&
                manager.dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_loopSize>() != null ){
                    manager.dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_loopSize>().counted = false;
                    manager.dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_loopSize>().shrink();
                     
                }

                else if(manager.dragging.GetComponentInChildren<VP_shadow>()!= null &&  
                manager.dragging.GetComponentInChildren<VP_shadow>().loopParent == loopParent
                && occupied == null && other != occupied){
                    if(this.name != "shad_ifT" && this.name != "shad_ifF" )
                    manager.dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_ifSize>().counted = false;
                    manager.dragging.GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_ifSize>().shrink();
                     
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