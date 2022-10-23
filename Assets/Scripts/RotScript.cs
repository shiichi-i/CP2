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
    public float num_rotations, num_speed;
    public InputField nm_rt, nm_spd;
    string temp_rt, temp_spd;
    bool onStart, setter;

    VP_ControlExecute control;
    AssignmentControl assign;
    VP_Start start;

    string[] choices_rot = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
    
    string[] choices_spd = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
    "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "-10"};

    Dropdown letter;


    void Start(){
        control = this.GetComponent<VP_ControlExecute>();
        letter = this.transform.GetChild(1).GetComponent<Dropdown>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        start = GameObject.Find("Start").GetComponent<VP_Start>();
        onStart = true;
    }

    void Update()
    {
        if(control.execute){
            if(!setter){
                FindAssign();
                setter = true;
            }
            
            if(this.GetComponentInChildren<VP_shadow>().loopParent != null){
                onStart = control.onStart;
            }
            
            if(onStart){
                setValuesSpeed();
                setValuesRot();
                if(num_rotations == 1101001){
                    rotZ = num_speed * 200f;
                    newRot = new Vector3(0f, 0f,rotZ);
                    if(rod[0] != null){
                        rod[0].transform.Rotate(newRot * Time.deltaTime);
                    }
                    if(rod[1] != null){
                        rod[1].transform.Rotate(newRot * Time.deltaTime);
                    }
                    control.done = false;
                }else{
                    if(rod[0] != null && rod[0].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot < num_rotations+1){
                        rotZ = num_speed * 200f;
                        newRot = new Vector3(0f, 0f,rotZ);
                        if(rod[0] != null){
                            rod[0].transform.Rotate(newRot * Time.deltaTime);
                        }
                        if(rod[1] != null){
                            rod[1].transform.Rotate(newRot * Time.deltaTime);
                        }
                    }else if(rod[0] != null && rod[0].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot == num_rotations+1){
                        control.done = true;
                        control.execute = false;
                        control.DarkColor();
                        
                        if(GetComponentInChildren<VP_shadow>().loopParent == null){
                            start.index++;
                        }else{
                            GetComponentInChildren<VP_shadow>().loopParent.GetComponent<LopScript>().b_indx++;
                            GetComponentInChildren<VP_shadow>().loopParent.GetComponent<LopScript>().Looper();
                        }
                        
                        
                    }
                }
            }
        }
        if(control.done){
            Reset();
        }

        if(!control.execute){
            onStart = true;
            setter = false;
        }
    }

    void Reset(){
        if(rod[0] != null){
            rod[0].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot = 0;
        }
        if(rod[1] != null){
            rod[1].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot = 0;
        }

    }


    public void setValuesSpeed(){
        temp_spd = nm_spd.text;
        bool enter = false;
        if(temp_spd != ""){
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
        control.done = true;
    }
    
}
