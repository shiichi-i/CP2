using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorControl : MonoBehaviour
{
    ObjSelection selection;
    public Text txt;
    Animator anim;

    public GameObject part, assign;

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
            if (selection.currentObj.GetComponent<ObjInfo>().isPart)
            {
                part.SetActive(true);
                assign.SetActive(false);
            }
            else
            {
                assign.SetActive(true);
                part.SetActive(false);
            }
        }
        else
        {
            part.SetActive(false);
            assign.SetActive(false);
            anim.SetBool("isOpen", false);
            txt.text = "";
            
        }
    }
}
