using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorControl : MonoBehaviour
{
    SpawnManager spawn;
    AvoidCollision collision;
    ObjSelection selection;
    public Text txt;
    Animator anim;

    public GameObject part, assign;
    GameObject inObj, outObj;


    public Slider m_SliderHue, m_SliderScale;
    public Toggle toggle;

    public Dropdown inDrop, outDrop;
    AssignmentControl assignment;

    public GameObject prop2;

    Renderer m_Renderer;

    void Start()
    {
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        anim = this.gameObject.GetComponent<Animator>();
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        assignment = this.gameObject.GetComponent<AssignmentControl>();

        //min-max values for hue and sat
        m_SliderHue.maxValue = 1;
        m_SliderHue.minValue = 0;

        inObj = inDrop.gameObject;
        outObj = outDrop.gameObject;
    }

    void Update()
    {

        if (selection.currentObj != null)
        {
            anim.SetBool("isOpen", true);
            txt.text = selection.currentObj.GetComponent<RigidBodyControls>().objType;
            if (selection.currentObj.GetComponent<ObjInfo>().isPart)
            {
                if(selection.currentObj.GetComponent<ObjInfo>().isMicrocontroller){
                    prop2.SetActive(false);
                }
                part.SetActive(true);
                assign.SetActive(false);
            }
            else
            {
                assign.SetActive(true);
                part.SetActive(false);
            }

            if(!selection.currentObj.GetComponent<ObjInfo>().isMicrocontroller){
                if (selection.currentObj.GetComponent<ObjInfo>().isSensor)
                {
                    inObj.SetActive(true);
                    outObj.SetActive(false);
                }
                else
                {
                    inObj.SetActive(false);
                    outObj.SetActive(true);
                }
            }
            else
            {
                inObj.SetActive(false);
                outObj.SetActive(false);
            }
            
        }
        else
        {
            part.SetActive(false);
            assign.SetActive(false);
            anim.SetBool("isOpen", false);
            txt.text = "";
        }

        if(spawn.willSpawn){
            part.SetActive(false);
            assign.SetActive(false);
            inObj.SetActive(false);
            outObj.SetActive(false);
        }

        
    }

    public void onColorChange(){
        selection.currentObj.GetComponent<ObjInfo>().col = m_SliderHue.value;
        
    }

    public void onTransparentChange(){
        selection.currentObj.GetComponent<ObjInfo>().transparent = toggle.isOn;
    }

    public void onScaleChange(){
        selection.currentObj.GetComponent<ObjInfo>().scale = m_SliderScale.value;
        if(selection.currentObj.tag != "Player"){

            GameObject arrow = selection.currentObj.transform.parent.gameObject;
            Transform rot = arrow.transform.Find("R-Y");
            

            Transform[] children = selection.currentObj.GetComponentsInChildren<Transform>();
            if (children != null){
                foreach (Transform c in children){
                    c.SetParent(null);
                }
            }
            OnScale();
            if (children != null){
                foreach (Transform c in children){
                    c.SetParent(selection.currentObj.transform);
                }
            }

            rot.transform.eulerAngles = selection.currentObj.transform.eulerAngles;
            selection.currentObj.transform.SetParent(arrow.transform);


        }else{
            GameObject temp = selection.currentObj.transform.parent.gameObject;
            selection.currentObj.transform.SetParent(null);
            OnScale();
            selection.currentObj.transform.SetParent(temp.transform);
        }
        
    }

    void OnScale(){
        selection.currentObj.transform.localScale = new Vector3(selection.currentObj.GetComponent<ObjInfo>().this_scale.x + selection.currentObj.GetComponent<ObjInfo>().scale * 10f,
        selection.currentObj.GetComponent<ObjInfo>().this_scale.y + selection.currentObj.GetComponent<ObjInfo>().scale * 10f,
        selection.currentObj.GetComponent<ObjInfo>().this_scale.z + selection.currentObj.GetComponent<ObjInfo>().scale * 10f);
    }

    public void OnAssignment(){
        if(selection.currentObj.GetComponent<ObjInfo>().isSensor){
            selection.currentObj.GetComponent<ObjInfo>().let_in = inDrop.value;
            assignment.CheckAvailable();
        }
            
        else{
            selection.currentObj.GetComponent<ObjInfo>().let_out = outDrop.value;
            assignment.CheckAvailable();
        }
            
    }
}
