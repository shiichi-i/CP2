using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{
    public bool isColliding = false;
    public GameObject selectedObj;
    public Material red, temp;
    SpawnManager spawn;
    ObjSelection moving;

    void Start()
    {
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        moving = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    void Update()
    {
        if (selectedObj != null)
        {
            if (isColliding)
            {
                if (moving.currentObj != null && moving.currentObj.GetComponent<ObjInfo>().isSpecial)
                {
                    for(int i = 0; i < selectedObj.transform.childCount; i++){
                            if( selectedObj.transform.GetChild(i).GetComponent<Renderer>() != null){
                                selectedObj.transform.GetChild(i).GetComponent<Renderer>().material = red;
                            }
                        }
                }
                else 
                {
                    if(selectedObj.GetComponent<Renderer>() != null){
                        selectedObj.GetComponent<Renderer>().material = red;
                    }
                }
            }
        }
    }
}
