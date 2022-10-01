using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotScript : MonoBehaviour
{
    public GameObject rod;
    float rotZ;
    public Vector3 newRot;
    public bool execute;
    public float num_rotations, num_speed;
    public InputField nm_rt, nm_spd;
    string temp_rt, temp_spd;

    VP_ControlExecute control;
    AssignmentControl assign;

    string[] choices = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "i"};

    Dropdown letter;


    void Start(){
        control = this.GetComponent<VP_ControlExecute>();
        letter = this.transform.GetChild(0).GetComponent<Dropdown>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
    }

    void Update()
    {
        if(control.execute){
            FindAssign();
                rotZ = num_speed * 200f;
                newRot = new Vector3(0f, 0f,rotZ);
            if(rod != null){
                rod.transform.Rotate(newRot * Time.deltaTime);
            }     
        }else if(!control.execute && rod != null){
            rod.transform.parent.Find("CountRot").GetComponent<CountRot>().fNum_rot = 0;
        }
    }

    public void setValuesSpeed(){
        temp_spd = nm_spd.text;
        bool enter = false;
        for(int i = 0; i < choices.Length-1; i++){
            if(temp_spd == choices[i]){
                temp_spd = nm_spd.text;
                enter = true;
            }
        }

        if(!enter){
            temp_spd = "0";
            nm_spd.text = "0";
        }

        float.TryParse(temp_spd, out num_speed);
        
    }

    public void setValuesRot(){
        temp_rt = nm_rt.text;
        bool enter = false;

        if(temp_rt == "i"){
            temp_rt = "11";
            nm_rt.text = "i";
            enter = true;
        }else{
            for(int i = 0; i < choices.Length; i++){
                if(temp_rt == choices[i]){
                    temp_rt = nm_rt.text;
                    enter = true;
                }
            }

        }   
            
        if(!enter){
            temp_rt = "0";
            nm_rt.text = "0";
        }

        float.TryParse(temp_rt, out num_rotations);
    }


    public void FindAssign(){
        for(int i = 0; i < assign.motors.Length; i++){
            if(assign.motors[i] != null){
                if(assign.outTake[i] == letter.value+6){
                    rod = assign.motors[i].transform.Find("rod").gameObject;
                }
            }
        }
    }

}
