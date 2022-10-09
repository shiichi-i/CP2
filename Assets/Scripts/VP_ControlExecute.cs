using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_ControlExecute : MonoBehaviour
{
    public bool execute;
    public bool done;
    SimManager sim;
    VP_Start start;

    void Start(){
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
        start = GameObject.Find("Start").GetComponent<VP_Start>();
    }

    void Update(){
        if(!sim.Playing){
            execute = false;
        }
        if(done){
            start.StartProgram();
            DarkColor();
            done = false;
        }
    }

    void DarkColor(){
        this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, 
        this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0.5f);
    }
}
