using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_LightScript : MonoBehaviour
{
    public Sens_Script sens;
    public GameObject sensorObj;

    VP_manager manager;

    public float nm_l;
    public InputField num_light;
    string temp_l;
    bool setter;

    SimManager sim;
    AssignmentControl assign;
    DaylightControl dc;
    public bool day, ret;

    string[] choices = {"0", "1"};

    Dropdown letter;

    void Start()
    {

        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();

        letter = this.transform.GetChild(1).GetComponent<Dropdown>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
        dc = GameObject.Find("Light").GetComponent<DaylightControl>();

    }

     void Update()
    {
            day = dc.Day;

            if(sensorObj != null){
                if(nm_l == 1 && day){
                    if(!sensorObj.GetComponentInChildren<dist_values>().isIn){
                        ret = true;
                    }else{
                        ret = false;
                    }
                }else if(nm_l == 0 && !day){
                        ret = true;
                }else if(nm_l == 1 && !day){
                        ret = false;
                }else if(nm_l == 0 && day){
                    if(sensorObj.GetComponentInChildren<dist_values>().isIn){
                        ret = true;
                    }else{
                        ret = false;
                    }
                }
                sens.ready = true;
            }else{
                ret = false;
            }
            sens.ret = ret;
            
            if(sim.Playing){
                if(!setter){
                    FindAssign();
                    setter = true;
                    if(sensorObj != null){
                        if(nm_l == 1 && day){
                            if(!sensorObj.GetComponentInChildren<dist_values>().isIn){
                                ret = true;
                            }else{
                                ret = false;
                            }
                        }else if(nm_l == 0 && !day){
                                ret = true;
                        }else if(nm_l == 1 && !day){
                                ret = false;
                        }else if(nm_l == 0 && day){
                            if(sensorObj.GetComponentInChildren<dist_values>().isIn){
                                ret = true;
                            }else{
                                ret = false;
                            }
                        }   
                    }else{
                        ret = false;
                    }
                    sens.ret = ret;
                }
            }

            if(transform.parent.name == "Panel" && manager.colliding != null && manager.colliding.name != "Trash"){
                GetComponentInChildren<Image>().color = new Color(1,1,1,1);
            }else if(manager.colliding != null && manager.colliding.name != "Trash"){
                GetComponentInChildren<Image>().color = transform.parent.parent.GetComponentInChildren<Image>().color;
            }

            if(!sim.Playing){
                setter = false;
            }
        
    }

    public void setValueLight(){
        FindAssign();
        temp_l = num_light.text;
        bool enter = false;
        if(temp_l != ""){
            for(int i = 0; i <= choices.Length-1; i++){
                if(temp_l == choices[i]){
                    temp_l = num_light.text;
                    enter = true;
                }
            }
        }

        if(!enter){
            temp_l = "0";
            num_light.text = "0";
        }

        float.TryParse(temp_l, out nm_l);
    }


    public void FindAssign(){
        sensorObj = null;
        for(int i = 0; i < assign.sensors.Length; i++){
            if(assign.sensors[i] != null){
                if(assign.inTake[i] == letter.value+6 && assign.sensors[i].name == "i_light(Clone)"){
                    sensorObj = assign.sensors[i];
                }
            }
        }
    }
}
