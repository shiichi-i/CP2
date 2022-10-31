using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disThisValues : MonoBehaviour
{
    public Vector3 thisScale, thisDist;
    public bool isIn;

    void Start(){
        thisScale = transform.localScale;
        thisDist = transform.localPosition;
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject != transform.parent.gameObject){
            Debug.Log("enter");
            isIn = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject != transform.parent.gameObject){
            Debug.Log("exit");
            isIn = false;
        }
    }
}
