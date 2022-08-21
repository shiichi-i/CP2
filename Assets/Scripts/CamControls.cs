using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    public float speed = 100F;

    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            transform.eulerAngles += speed * new Vector3(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0f);
        }
    }
}