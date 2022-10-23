using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_ControlExecute : MonoBehaviour
{
    public bool execute;
    public bool done, onStart;
    SimManager sim;
    VP_Start start;

    void Start(){
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
        start = GameObject.Find("Start").GetComponent<VP_Start>();
    }

    void Update(){
        if(!sim.Playing){
            execute = false;
            onStart = true;
        }

        if(done){
            execute = false;
            if(GetComponent<LopScript>() == null){
                start.StartProgram();
            }else if(transform.GetComponentInChildren<VP_shadow>().loopParent == null){
                start.StartProgram();
            }

            DarkColor();
            done = false;
        }
    }

    void DarkColor(){
        this.transform.GetChild(0).GetComponent<Image>().color = new Color(this.transform.GetChild(0).GetComponent<Image>().color.r, 
        this.transform.GetChild(0).GetComponent<Image>().color.g, this.transform.GetChild(0).GetComponent<Image>().color.b, 0.5f);
        if(this.gameObject.GetComponent<LopScript>() != null){
            this.transform.GetChild(2).GetComponent<Image>().color = new Color(this.transform.GetChild(2).GetComponent<Image>().color.r, 
            this.transform.GetChild(2).GetComponent<Image>().color.g, this.transform.GetChild(2).GetComponent<Image>().color.b, 0.5f);

            this.transform.GetChild(3).GetComponent<Image>().color = new Color(this.transform.GetChild(3).GetComponent<Image>().color.r, 
            this.transform.GetChild(3).GetComponent<Image>().color.g, this.transform.GetChild(3).GetComponent<Image>().color.b, 0.5f);
        }
    }
}
