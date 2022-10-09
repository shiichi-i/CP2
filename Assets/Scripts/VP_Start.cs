using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VP_Start : MonoBehaviour
{
    public GameObject[] codes;
    SimManager sim;
    int blockSize = 0;
    bool count;
    public int index;

    void Start(){
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    public void CountBlocks(){
        GameObject bock = this.gameObject;
        count = true;
        index = 0;
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
    }

    public void ChangeColor(){
        for(int i = 0; i < blockSize; i++){
            codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color.r, codes[i].gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
            codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color.b, 0.5f);
        }
    }

    public void StartProgram(){
        if(index < blockSize){
            if(!codes[index].GetComponent<VP_ControlExecute>().execute && !codes[index].GetComponent<VP_ControlExecute>().done)
            { 
                codes[index].GetComponent<VP_ControlExecute>().execute = true;
                codes[index].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(codes[index].transform.GetChild(0).gameObject.GetComponent<Image>().color.r, codes[index].gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
                codes[index].transform.GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
            }
        }
    }

    public void EndProgram(){
        for(int i = 0; i < blockSize; i++){
            codes[i].GetComponent<VP_ControlExecute>().execute = false;
            codes[i].GetComponent<VP_ControlExecute>().done = false;
            codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color.r, codes[i].gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
            codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
        }

        blockSize = 0;
        for(int c =0; c < codes.Length; c++){
            codes[c] = null;
        }
    }
}
