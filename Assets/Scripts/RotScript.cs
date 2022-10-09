using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotScript : MonoBehaviour
{
    public GameObject[] rod;
    int rod_i;
    float rotZ;
    public Vector3 newRot;
    public bool execute;
    public float num_rotations, num_speed;
    public InputField nm_rt, nm_spd;
    string temp_rt, temp_spd;
    bool onStart;

    VP_ControlExecute control;
    AssignmentControl assign;
    VP_Start start;

    string[] choices_rot = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
    
    string[] choices_spd = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
    "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "-10"};

    Dropdown letter;


    void Start(){
        control = this.GetComponent<VP_ControlExecute>();
        letter = this.transform.GetChild(0).GetComponent<Dropdown>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        start = GameObject.Find("Start").GetComponent<VP_Start>();
    }

    void Update()
    {
        if(control.execute){
            FindAssign();
            if(onStart){
                onStart = false;
                if(num_rotations == 1101001){
                    rotZ = num_speed * 200f;
                    newRot = new Vector3(0f, 0f,rotZ);
                    if(rod[0] != null){
                        rod[0].transform.Rotate(newRot * Time.deltaTime);
                    }
                    if(rod[1] != null){
                        rod[1].transform.Rotate(newRot * Time.deltaTime);
                    }
                    }
                    control.done = false;
                }else{
                    if(rod[0] != null && rod[0].transform.parent.GetChild(0).GetChild(0).GetComponent<CountRot>().fNum_rot < num_rotations){
                        rotZ = num_speed * 200f;
                        newRot = new Vector3(0f, 0f,rotZ);
                        if(rod[0] != null){
                            rod[0].transform.Rotate(newRot * Time.deltaTime);
                        }
                        if(rod[1] != null){
                            rod[1].transform.Rotate(newRot * Time.deltaTime);
                        }
                    }else if(rod[0] != null && rod[0].transform.parent.GetChild(0).GetChild(0).GetComponent<CountRot>().fNum_rot == num_rotations){
                        control.done = true;
                        start.index++;
                        control.execute = false;
                    }
                }
            }else if(!control.execute && rod[0] != null){
                if(rod[0] != null){
                    rod[0].transform.parent.GetChild(0).GetChild(0).GetComponent<CountRot>().fNum_rot = 0;
                }
                if(rod[1] != null){
                   rod[1].transform.parent.GetChild(0).GetChild(0).GetComponent<CountRot>().fNum_rot = 0;
                }
            }
                

        if(!control.execute){
            onStart = true;
        }
    }


    public void setValuesSpeed(){
        temp_spd = nm_spd.text;
        bool enter = false;
        if(temp_rt != ""){
            for(int i = 0; i <= choices_spd.Length-1; i++){
                if(temp_spd == choices_spd[i]){
                    temp_spd = nm_spd.text;
                    enter = true;
                }
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

        if(temp_rt != ""){
            if(temp_rt == "i"){
                temp_rt = "1101001";
                nm_rt.text = "i";
                enter = true;
            }else{
                for(int i = 0; i < choices_rot.Length; i++){
                    if(temp_rt == choices_rot[i]){
                        temp_rt = nm_rt.text;
                        enter = true;
                    }
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
        for(int i = 0; i < rod_i ;i++){
            rod[i] = null;
        }
        rod_i = 0;
        for(int i = 0; i < assign.motors.Length; i++){
            if(assign.motors[i] != null){
                if(assign.outTake[i] == letter.value+6){
                    rod[rod_i] = assign.motors[i].transform.Find("rod").gameObject;
                    rod_i++;
                }else{
                    if(letter.value+6 == 10){
                        if(assign.outTake[i] == 6 || assign.outTake[i] == 7){
                            rod[rod_i] = assign.motors[i].transform.Find("rod").gameObject;
                            rod_i++;
                        }
                    }else if(letter.value+6 == 11){
                        if(assign.outTake[i] == 8 || assign.outTake[i] == 9){
                            rod[rod_i] = assign.motors[i].transform.Find("rod").gameObject;
                            rod_i++;
                        }
                    }
                }
            }
        }
    }
    
}
