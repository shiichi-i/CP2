using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dist_values : MonoBehaviour
{
    public Vector3 thisScale, thisDist;
    public bool isIn;

    void Start(){
        thisScale = transform.localScale;
        thisDist = transform.localPosition;
    }

    public void ResetSize(){
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, thisScale.z);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, thisDist.z);
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject != transform.parent.gameObject && other.tag == "Selectable"){
            isIn = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject != transform.parent.gameObject && other.tag == "Selectable"){
            isIn = false;
        }
    }
}
