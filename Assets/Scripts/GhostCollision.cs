using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    AvoidCollision col;

    void Start()
    {
        col = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag != "CodeArea" && other.name != "rod" && other.tag != "Untagged" && other.tag != "Player" && other.name != "pb_wheel(Clone)"){
            col.ghostCol = true;
            this.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
        }else{
            col.ghostCol = false;
        }
    }

    void OnTriggerExit(Collider other){
        col.ghostCol = false;
        this.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
    }
}
