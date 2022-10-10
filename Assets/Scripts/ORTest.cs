using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORTest : MonoBehaviour
{
    public GameObject rodObj;
    public bool nega;

    void Update()
    {
        if(nega){
            Vector3 newRot = new Vector3(0f, 0f, -1000);
            rodObj.transform.Rotate(newRot * Time.deltaTime);
        }else{
            Vector3 newRot = new Vector3(0f, 0f, 1000);
            rodObj.transform.Rotate(newRot * Time.deltaTime);
        }
    }
}
