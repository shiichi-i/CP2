using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_ControlExecute : MonoBehaviour
{
    public bool execute;
    public bool done;
    SimManager sim;

    void Start(){
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    void Update(){
        if(!sim.Playing){
            execute = false;
        }
    }
}
