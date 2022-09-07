using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotate : MonoBehaviour
{
    public GameObject arrows;
    public GameObject tempObj = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Selectable")
                {
                    tempObj = hit.collider.gameObject;
                    Instantiate(arrows, new Vector3((float)(tempObj.transform.position.x - 2.2), (float)(tempObj.transform.position.y + 0.2), (float)(tempObj.transform.position.z - 1.7)), Quaternion.identity, tempObj.transform);
                }
            }
        }
    }
}
