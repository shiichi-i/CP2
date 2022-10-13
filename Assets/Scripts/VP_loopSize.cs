using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_loopSize : MonoBehaviour
{
    VP_manager manager;
    float thisWidth;
    public bool counted;
    public GameObject loopm, loope;
    public float totWidth, total, children, totchildren;

    void Start(){
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
        thisWidth = this.GetComponent<RectTransform>().sizeDelta.x;
        total = 0;
        totWidth = thisWidth;
        totchildren = 0;
    }

    public void counter(bool inside){
        GameObject startC = manager.dragging;
        children = 0;
        total = thisWidth;
            if(inside && manager.dropped){
                startC.GetComponentInChildren<VP_shadow>().inSide = true;
            }else if(!inside && manager.dropped){
                startC.GetComponentInChildren<VP_shadow>().inSide = false;
            }
            
            total += startC.GetComponent<RectTransform>().sizeDelta.x - 50f;
            children++;

            bool count = true;
            int i = 0; 
            while(count){
                if(i < startC.transform.childCount && startC.transform.GetChild(i).tag == "Player"){
                    if(inside){
                        startC.transform.GetChild(i).GetComponentInChildren<VP_shadow>().inSide = true;
                    }else{
                        startC.transform.GetChild(i).GetComponentInChildren<VP_shadow>().inSide = false;
                    }
                    total += startC.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.x - 15f;
                    startC = startC.transform.GetChild(i).gameObject; 
                    i = 0;
                    children++;
                }else if(i == startC.transform.childCount-1){
                    count = false;
                }else{
                    i++;
                }
            }

    }

    public void extend(){  
        if(manager.dragging != null && !counted){
            counter(true);
            totWidth += total; 
            totchildren += children;
            grow();
        }
    }

    void grow(){
        if(loope != null){
            loope.transform.localPosition = new Vector3(totWidth, loope.transform.localPosition.y, loope.transform.localPosition.z);
            loopm.transform.localPosition = new Vector3((totWidth/2) - thisWidth, loopm.transform.localPosition.y, loopm.transform.localPosition.z);
            loopm.GetComponent<RectTransform>().sizeDelta = new Vector2(totWidth, 
            loopm.GetComponent<RectTransform>().sizeDelta.y);
            counted = true;
            Debug.Log("taken: "+children+ ",,//,,Total: "+totchildren);
        }
    }

    public void shrink(){
        if(manager.dragging != null && !counted && children > 0){
            counter(false);
            totWidth -= total;
            totchildren -= children;
            grow();
        }
    }

}
