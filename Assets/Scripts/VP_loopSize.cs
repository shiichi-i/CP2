using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_loopSize : MonoBehaviour
{
    VP_manager manager;
    float thisWidth;
    public bool counted;
    public GameObject loopm, loope;
    public float totWidth, total;

    void Start(){
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();
        thisWidth = this.GetComponent<RectTransform>().sizeDelta.x;
        total = 0;
        totWidth = thisWidth;
    }
    void counter(bool inside){
        GameObject startC = manager.dragging;
        float children = 0;
        total = thisWidth;
            if(inside){
                manager.dragging.GetComponentInChildren<VP_shadow>().inSide = true;
            }else{
                manager.dragging.GetComponentInChildren<VP_shadow>().inSide = false;
            }
            total += manager.dragging.GetComponent<RectTransform>().sizeDelta.x - 50f;

            bool count = true;
            int i = 0; 
            while(count){
                if(i < startC.transform.childCount && startC.transform.GetChild(i).tag == "Player"){
                    total += startC.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.x - 15f;
                    startC = startC.transform.GetChild(i).gameObject;
                    if(inside){
                        manager.dragging.transform.GetChild(i).GetComponentInChildren<VP_shadow>().inSide = true;
                    }else{
                        manager.dragging.transform.GetChild(i).GetComponentInChildren<VP_shadow>().inSide = false;
                    }
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
        }
    }

    public void shrink(){
        if(manager.dragging != null && !counted){
            counter(false);
            totWidth -= total;
            grow();
        }
    }

}
