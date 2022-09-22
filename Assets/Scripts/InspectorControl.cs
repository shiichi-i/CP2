using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorControl : MonoBehaviour
{
    ObjSelection selection;
    public Text txt;
    Animator anim;

    void Start()
    {
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(selection.currentObj != null)
        {
            anim.SetBool("isOpen", true);
            txt.text = selection.currentObj.GetComponent<RigidBodyControls>().objType;
        }
        else
        {
            anim.SetBool("isOpen", false);
            txt.text = "";
        }
    }
}
