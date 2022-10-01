using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountRot : MonoBehaviour
{
    public float fNum_rot;

    void OnTriggerEnter(Collider other){
        if(other.tag == "Untagged"){
            fNum_rot++;
        }
    }
}
