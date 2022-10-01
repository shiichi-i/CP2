using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_Start : MonoBehaviour
{
    public GameObject[] codes;
    SimManager sim;
    int blockSize = 0;
    bool count;

    void Start(){
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    public void CountBlocks(){
        GameObject bock = this.gameObject;
        count = true;
        int i = 0; 
        while(count){   
            if(i < bock.transform.childCount && bock.transform.GetChild(i).tag == "Player"){
                bock = bock.transform.GetChild(i).gameObject;
                i = 0;
                codes[blockSize] = bock;
                blockSize++;
            }else if(i == bock.transform.childCount-1){
                count = false;
            }else{
                i++;
            }
        }

        StartProgram();
    }

    void StartProgram(){
        for(int i = 0; i < blockSize; i++){
            if(!codes[i].GetComponent<VP_ControlExecute>().done){
                codes[i].GetComponent<VP_ControlExecute>().execute = true;
            }else{
                i++;
            }
        }
    }
}
