using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorControl : MonoBehaviour
{
    ObjSelection selection;
    public Text txt;

    void Start()
    {
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    void Update()
    {
        if(selection.currentObj != null)
        {
            txt.text = selection.currentObj.GetComponent<RigidBodyControls>().objType;
        }
        else
        {
            txt.text = "";
        }
    }
}
