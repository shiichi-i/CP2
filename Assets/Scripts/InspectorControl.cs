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
    //hue
    public Slider m_SliderHue;

    Renderer m_Renderer;

    void Start()
    {
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        anim = this.gameObject.GetComponent<Animator>();

        //min-max values for hue and sat
        m_SliderHue.maxValue = 1;
        m_SliderHue.minValue = 0;
    }

    void Update()
    {

        if (selection.currentObj != null)
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

            //hue
            m_Renderer = selection.currentObj.GetComponent<Renderer>();
            //made slider change saturation too so we can have white mesh color
            m_Renderer.material.color = Color.HSVToRGB(m_SliderHue.value, m_SliderHue.value, 1);
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
