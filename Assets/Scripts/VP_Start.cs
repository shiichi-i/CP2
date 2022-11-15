using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VP_Start : MonoBehaviour
{
    public GameObject[] codes;
    public SimManager sim;
    int blockSize = 0;
    bool count;
    public int index;
    public bool tut3;

    public GameObject tutorial;

    void Start(){
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    public void CountBlocks(){
        GameObject bock = this.gameObject;
        count = true;
        
        int i = 0;

        while(count){   
            if(i < bock.transform.childCount && bock.transform.GetChild(i).GetComponent<LopScript>() == null &&
                bock.transform.GetChild(i).GetComponent<IfScript>() == null &&
                bock.transform.GetChild(i).tag == "Player"){

                    bock = bock.transform.GetChild(i).gameObject;
                    codes[blockSize] = bock;
                    blockSize++;
                    i = 0;

            }else if(i < bock.transform.childCount && bock.transform.GetChild(i).GetComponent<IfScript>() != null){

                codes[blockSize] = bock.transform.GetChild(i).gameObject;
                blockSize++;

                if(bock.transform.GetChild(i).Find("BackGround (1)").childCount > 1){
                    bock = bock.transform.GetChild(i).Find("BackGround (1)").GetChild(1).gameObject;
                    codes[blockSize] = bock;
                    blockSize++;
                    i = 0;
                }else{
                    count = false;
                }

            }else if(i < bock.transform.childCount && bock.transform.GetChild(i).GetComponent<LopScript>() != null){
                    
                    codes[blockSize] = bock.transform.GetChild(i).gameObject;
                    blockSize++;

                    if(bock.transform.GetChild(i).Find("BackGround (1)").childCount > 2){
                        bock = bock.transform.GetChild(i).Find("BackGround (1)").GetChild(2).gameObject;
                        codes[blockSize] = bock;
                        blockSize++;
                        i = 0;
                    }else{
                        count = false;
                    }
                    

            }else if(i == bock.transform.childCount-1){
                count = false;
            }else{
                i++;
            }
        }
    }

    void Update(){
        if(!sim.Playing){
            index = 0;
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
        }else{
            if(!tut3 && tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 4){
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().NextTut();
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
            }
            if(tut3 && tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 2){
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().NextTut();
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
            }
        }
    }

    public void EndProgram(){
        for(int i = 0; i < blockSize; i++){
            codes[i].GetComponent<VP_ControlExecute>().execute = false;
            codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color.r, codes[i].gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
            codes[i].transform.GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
        }
        
        blockSize = 0;
        for(int c =0; c < codes.Length; c++){
            codes[c] = null;
        }
    }
}
