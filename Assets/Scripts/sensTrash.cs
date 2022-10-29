using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensTrash : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other){
        if(other.name == "Trash"){
            Debug.Log("detection");
        }
    }
}
