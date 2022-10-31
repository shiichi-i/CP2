using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Distance : MonoBehaviour
{
    Sens_Script sens;
    public GameObject sensorObj;
    public bool ret;

    public float dist=1;

    VP_manager manager;

    SimManager sim;
    AssignmentControl assign;
    Dropdown letter;

    public float nm_l = 1;
    public InputField num_light;
    string temp_l;
    bool setter;

    string[] choices = {"1", "2", "3", "4", "5"};

    void Start()
    {
        manager = GameObject.Find("CodeArea").GetComponent<VP_manager>();

        sens = GetComponent<Sens_Script>();

        letter = this.transform.GetChild(1).GetComponent<Dropdown>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    void ChangeSize(){
        sensorObj.GetComponentInChildren<dist_values>().ResetSize();

        sensorObj.transform.GetChild(0).localScale = new Vector3(sensorObj.transform.GetChild(0).localScale.x, sensorObj.transform.GetChild(0).localScale.y,
        sensorObj.transform.GetChild(0).localScale.z+ dist/100);

        sensorObj.transform.GetChild(0).localPosition = new Vector3(sensorObj.transform.GetChild(0).localPosition.x, sensorObj.transform.GetChild(0).localPosition.y,
        sensorObj.transform.GetChild(0).localPosition.z- dist/300);
    }

    void Update()
    {
        if(sensorObj != null){
            ret = sensorObj.GetComponentInChildren<dist_values>().isIn;
        }

        if(sim.Playing){
                if(!setter){
                    FindAssign();
                    setter = true;
                    if(sensorObj != null){
                        ChangeSize();
                        ret = sensorObj.GetComponentInChildren<dist_values>().isIn;
                        sens.ready = true;
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

            if(transform.parent.name == "Panel" && manager.colliding != null && manager.colliding.name != "Trash"){
                GetComponentInChildren<Image>().color = new Color(1,1,1,1);
            }else if(manager.colliding != null && manager.colliding.name != "Trash"){
                GetComponentInChildren<Image>().color = transform.parent.parent.GetComponentInChildren<Image>().color;
            }

            if(!sim.Playing){
                setter = false;
            }
    }

    public void setValueInch(){
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
            temp_l = "1";
            num_light.text = "1";
        }

        float.TryParse(temp_l, out dist);
    }

    public void FindAssign(){
        sensorObj = null;
        for(int i = 0; i < assign.sensors.Length; i++){
            if(assign.sensors[i] != null){
                if(assign.inTake[i] == letter.value+6 && assign.sensors[i].name == "i_distance(Clone)"){
                    sensorObj = assign.sensors[i];
                }
            }
        }
    }
}
