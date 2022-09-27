using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentControl : MonoBehaviour
{
    InspectorControl inspector;
    public int motorCount, sensorCount;
    public Button[] sTab, mTab;
    public GameObject[] motors, sensors;
    public int[] inTake, outTake;

    ObjSelection selection;

    void Start(){
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        inspector = this.gameObject.GetComponent<InspectorControl>();
    }

    void Update(){
        if(motorCount == 4){
            for(int i = 0; i < mTab.Length; i++){
                mTab[i].interactable = false;
                Image icon = mTab[i].transform.GetChild(0).GetComponent<Image>();
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
            }
        }
        else if(sensorCount == 4){
            for(int i = 0; i < sTab.Length; i++){
                sTab[i].interactable = false;
                Image icon = sTab[i].transform.GetChild(0).GetComponent<Image>();
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
            }
        }
        else{
            for(int i = 0; i < mTab.Length; i++){
                mTab[i].interactable = true;
                Image icon = mTab[i].transform.GetChild(0).GetComponent<Image>();
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
            }
            for(int i = 0; i < sTab.Length; i++){
                sTab[i].interactable = true;
                Image icon = sTab[i].transform.GetChild(0).GetComponent<Image>();
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
            }
        }
    }

    public void CheckAvailable(){
        if(selection.currentObj.GetComponent<ObjInfo>().isSensor){
            for(int i =0; i < sensors.Length ;i++){
                if(sensors[i] != null){
                    inTake[i] = sensors[i].GetComponent<ObjInfo>().let_in + 5;
                }
            }
        }else{
            for(int i =0; i < motors.Length ;i++){
                if(motors[i] != null){
                    outTake[i] = motors[i].GetComponent<ObjInfo>().let_out + 5;
                }
            }
        }
    }

}
