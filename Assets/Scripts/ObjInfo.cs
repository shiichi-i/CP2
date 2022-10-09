using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInfo : MonoBehaviour
{
    public bool isSpecial;
    public bool isPart, isSensor, isMicrocontroller, isMotor, isMerged, isParent;
    public bool connected;
    public GameObject connection;
    public GameObject special;

    public string objType;
    public float col;
    public bool transparent;
    public int let_in, let_out;
    public string assignment;
    public float scale;
    public Vector3 this_scale;
    ObjSelection select;
    InspectorControl inspector;
    AvoidCollision collision;
    SpawnManager spawn;
    
    void Start(){
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        inspector = GameObject.Find("Inspector").GetComponent<InspectorControl>();
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        spawn = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        this_scale = this.gameObject.transform.localScale;
        if(isSpecial && !isParent){
            special = this.gameObject.transform.parent.gameObject;
        }
        
    }

    void Update(){
        if(isPart && select.currentObj == this.gameObject && !collision.isColliding){
            inspector.m_SliderHue.value = col;
            inspector.toggle.isOn = transparent;
            inspector.m_SliderScale.value = scale;
            
            if(transparent){
                this.gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(col, col, 1);
                this.gameObject.GetComponent<Renderer>().material.color = new Color(this.gameObject.GetComponent<Renderer>().material.color.r, this.gameObject.GetComponent<Renderer>().material.color.g, 
                this.gameObject.GetComponent<Renderer>().material.color.b, 0.5f);
            }else{
                this.gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(col, col, 1);
                this.gameObject.GetComponent<Renderer>().material.color = new Color(this.gameObject.GetComponent<Renderer>().material.color.r, this.gameObject.GetComponent<Renderer>().material.color.g, 
                this.gameObject.GetComponent<Renderer>().material.color.b, 1f);
            }
        }
        else if(!isPart && select.currentObj == this.gameObject && !collision.isColliding && !spawn.willSpawn ){

            if(isSensor){
                inspector.inDrop.value = let_in;
                assignment = inspector.inDrop.options[inspector.inDrop.value].text;
            }
            else if(!isSensor){
                inspector.outDrop.value = let_out;
                assignment = inspector.outDrop.options[inspector.outDrop.value].text;
            }

        }
        
    }

    public void SetColor(){
        if(isPart){
            if(transparent){
                this.gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(col, col, 1);
                this.gameObject.GetComponent<Renderer>().material.color = new Color(this.gameObject.GetComponent<Renderer>().material.color.r, this.gameObject.GetComponent<Renderer>().material.color.g, 
                this.gameObject.GetComponent<Renderer>().material.color.b, 0.5f);
            }else{
                this.gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(col, col, 1);
                this.gameObject.GetComponent<Renderer>().material.color = new Color(this.gameObject.GetComponent<Renderer>().material.color.r, this.gameObject.GetComponent<Renderer>().material.color.g, 
                this.gameObject.GetComponent<Renderer>().material.color.b, 1f);
            }
        }
    }


}
