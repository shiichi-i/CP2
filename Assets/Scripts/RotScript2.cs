using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotScript2 : MonoBehaviour
{
    public GameObject[] rod;
    public GameObject[] rod2;

    int rod_i, rod_i2;
    float rotZ, rotZ2;

    public Vector3 newRot, newRot2;

    public bool execute;

    public float num_rotations, num_speed, num_rotations2, num_speed2;
    public InputField nm_rt, nm_spd, nm_rt2, nm_spd2;
    string temp_rt, temp_spd, temp_rt2, temp_spd2;
    bool onStart, setA, setB, setter;

    VP_ControlExecute control;
    AssignmentControl assign;
    VP_Start start;
    SimManager sim;

    string[] choices_rot = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
    
    string[] choices_spd = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
    "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "-10"};

    public Dropdown letter, letter2;
    public int dval, dval2;

    public void SetText2(){
        nm_rt.text = num_rotations.ToString();
        nm_spd.text = num_speed.ToString();
        letter.value = dval;

        nm_rt2.text = num_rotations2.ToString();
        nm_spd2.text = num_speed2.ToString();
        letter2.value = dval2;
    }

    void Start(){
        control = this.GetComponent<VP_ControlExecute>();
        letter = this.transform.GetChild(1).GetComponent<Dropdown>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        start = GameObject.Find("Start").GetComponent<VP_Start>();
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();

    }

    void Update()
    {
        if(control.execute){
            if(!setter){
                FindAssign();
                setter = true;
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
                        setA = true;
                    }
                }

                if(num_rotations2 == 1101001){
                    rotZ2 = num_speed2 * 200f;
                    newRot2 = new Vector3(0f, 0f,rotZ2);
                    if(rod2[0] != null){
                        rod2[0].transform.Rotate(newRot2 * Time.deltaTime);
                    }
                    if(rod2[1] != null){
                        rod2[1].transform.Rotate(newRot2 * Time.deltaTime);
                    }
                    control.done = false;
                }else{
                    if(rod2[0] != null && rod2[0].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot < num_rotations2+1){
                        rotZ2 = num_speed2 * 200f;
                        newRot2 = new Vector3(0f, 0f,rotZ2);
                        if(rod2[0] != null){
                            rod2[0].transform.Rotate(newRot2 * Time.deltaTime);
                        }
                        if(rod2[1] != null){
                            rod2[1].transform.Rotate(newRot2 * Time.deltaTime);
                        }
                    }else if(rod2[0] != null && rod2[0].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot == num_rotations2+1){
                        setB = true;
                    }
                }

                if(setA && setB){
                        control.done = true;
                        control.execute = false;
                        control.DarkColor();
                        if(GetComponentInChildren<VP_shadow>().loopParent == null){
                            start.index++;
                        }else{
                            if(GetComponentInChildren<VP_shadow>().loopParent.GetComponentInChildren<VP_shadow>().isLoopParent){
                                GetComponentInChildren<VP_shadow>().loopParent.GetComponent<LopScript>().b_indx++;
                                GetComponentInChildren<VP_shadow>().loopParent.GetComponent<LopScript>().Looper();
                            }else{
                                GetComponentInChildren<VP_shadow>().loopParent.GetComponent<IfScript>().b_indx++;
                                GetComponentInChildren<VP_shadow>().loopParent.GetComponent<IfScript>().Returner();
                            }
                        }
                        
                }else if(!setA && !setB){
                    control.done = false;
                }

                
            }
        }

        if(control.done){
            Reset();
        }

        if(!control.execute){
            onStart = true;
            setA = false;
            setB = false;
            setter = false;
        }

        if(!sim.Playing){
            Reset();
        }
    }

    void Reset(){
        if(rod[0] != null){
            rod[0].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot = 0;
        }
        if(rod[1] != null){
            rod[1].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot = 0;
        }
        if(rod2[0] != null){
            rod2[0].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot = 0;
        }
        if(rod2[1] != null){
            rod2[1].transform.parent.transform.Find("CountRot").GetComponent<CountRot>().fNum_rot = 0;
        }

    }


    public void setValuesSpeed(){
        temp_spd = nm_spd.text;
        temp_spd2 = nm_spd2.text;
        bool enter = false, enter2= false;
        if(temp_spd != ""){
            for(int i = 0; i <= choices_spd.Length-1; i++){
                if(temp_spd == choices_spd[i]){
                    temp_spd = nm_spd.text;
                    enter = true;
                }
            }
        }
        if(temp_spd2 != ""){
            for(int i = 0; i <= choices_spd.Length-1; i++){
                if(temp_spd2 == choices_spd[i]){
                    temp_spd2 = nm_spd2.text;
                    enter2 = true;
                }
            }
        }

        if(!enter){
            temp_spd = "0";
            nm_spd.text = "0";
        }
        if(!enter2){
            temp_spd2 = "0";
            nm_spd2.text = "0";
        }

        float.TryParse(temp_spd, out num_speed);
        float.TryParse(temp_spd2, out num_speed2);
        
    }

    public void setValuesRot(){
        temp_rt = nm_rt.text;
        temp_rt2 = nm_rt2.text;
        bool enter = false, enter2= false;

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

        if(temp_rt2 != ""){
            if(temp_rt2 == "i"){
                temp_rt2 = "1101001";
                nm_rt2.text = "i";
                enter2 = true;
            }else{
                for(int i = 0; i < choices_rot.Length; i++){
                    if(temp_rt2 == choices_rot[i]){
                        temp_rt2 = nm_rt2.text;
                        enter2 = true;
                    }
                }

            }   
        }
            
        if(!enter){
            temp_rt = "0";
            nm_rt.text = "0";
        }
        if(!enter2){
            temp_rt2 = "0";
            nm_rt2.text = "0";
        }


        float.TryParse(temp_rt, out num_rotations);
        float.TryParse(temp_rt2, out num_rotations2);
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

        for(int i = 0; i < rod_i2 ;i++){
            rod2[i] = null;
        }
        rod_i2 = 0;
        for(int i = 0; i < assign.motors.Length; i++){
            if(assign.motors[i] != null){
                if(assign.outTake[i] == letter2.value+6){
                    rod2[rod_i2] = assign.motors[i].transform.Find("rod").gameObject;
                    rod_i2++;
                }else{
                    if(letter2.value+6 == 10){
                        if(assign.outTake[i] == 6 || assign.outTake[i] == 7){
                            rod2[rod_i2] = assign.motors[i].transform.Find("rod").gameObject;
                            rod_i2++;
                        }
                    }else if(letter2.value+6 == 11){
                        if(assign.outTake[i] == 8 || assign.outTake[i] == 9){
                            rod2[rod_i2] = assign.motors[i].transform.Find("rod").gameObject;
                            rod_i2++;
                        }
                    }
                }
            }
        }
        control.done = true;
    }

    public void Avail1(){
        int choice1, choice2;
        choice1 = letter.value;
        choice2 = letter2.value;
        for(int i = 0; i < 6; i++){
            letter.GetComponentsInChildren<Toggle>()[i].interactable = true;
        }
        letter.GetComponentsInChildren<Toggle>()[choice2].interactable = false;
    }

    public void Avail2(){
        int choice1, choice2;
        choice1 = letter.value;
        choice2 = letter2.value;
        for(int i = 0; i < 6; i++){
            letter2.GetComponentsInChildren<Toggle>()[i].interactable = true;
        }
        letter2.GetComponentsInChildren<Toggle>()[choice1].interactable = false;
    }
}
