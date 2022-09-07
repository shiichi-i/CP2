using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardingX : MonoBehaviour
{
    Vector3 cameraDir;

    void Update()
    {
            cameraDir = Camera.main.transform.forward;
            cameraDir.x = 0;

            transform.rotation = Quaternion.LookRotation(cameraDir);
    }

    public void click()
    {
            Debug.Log("Clicked X");
    }
}
