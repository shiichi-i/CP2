using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDropdown : MonoBehaviour
{
    ObjSelection select;
    AssignmentControl assignment;
    public Toggle[] choices;

    void Start(){
        assignment = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    public void OnOpen(){
        choices = this.gameObject.GetComponent<Dropdown>().GetComponentsInChildren<Toggle>();

        if(select.currentObj.GetComponent<ObjInfo>().isSensor){
            for(int i = 0; i < 4; i++){
                    if(assignment.inTake[i] > 5){
                        int temp = assignment.inTake[i] - 5;
                        choices[temp].interactable = false;
                    }
            }
        }
        else{
            for(int i = 0; i < 4; i++){
                if(assignment.outTake[i] > 5){
                    int temp = assignment.outTake[i] - 5;
                    choices[temp].interactable = false;
                }
            }
        }
    }
}
