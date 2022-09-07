using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardingY : MonoBehaviour
{
    Vector3 cameraDir;

    void Update()
    {
            cameraDir = Camera.main.transform.forward;
            cameraDir.z = 0;

            transform.rotation = Quaternion.LookRotation(cameraDir);
    }
}
