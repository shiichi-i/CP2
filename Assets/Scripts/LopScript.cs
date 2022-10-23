using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LopScript : MonoBehaviour
{
    VP_ControlExecute control;
    VP_Start start;
    public float iterations;
    public bool onStart, setter, count, starter;
    string iter_txt, tmp_i;
    public InputField nm_i;
    int j =0;

    public GameObject[] blockIn;
    public int b_indx, c_indx;

    GameObject startBlock;

    string[] choices_lp = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "i"};

    void Start()
    {
        onStart = true;
        control = this.GetComponent<VP_ControlExecute>();
        start = GameObject.Find("Start").GetComponent<VP_Start>();
        starter = true;
        iterations = 0;
        b_indx = 0;
        startBlock = transform.Find("shad_Loop_in").gameObject;
    }

    void countIn(){
        if(transform.Find("shad_Loop_in").childCount > 0){
            while(count){
                if(j < startBlock.transform.childCount && startBlock.transform.GetChild(j).tag == "Player"){
                    
                    startBlock.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color = new Color(startBlock.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color.r,
                    startBlock.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color.g, startBlock.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color.b, 0.5f);

                    startBlock = startBlock.transform.GetChild(j).gameObject;
                    blockIn[c_indx] = startBlock;
                    j = 0;
                    c_indx++;
                }else if(j == startBlock.transform.childCount-1){
                    count = false;
                }else{
                    j++;
                }
            }
        }
    }

    public void StartP(){
        blockIn[b_indx].GetComponent<VP_ControlExecute>().onStart = true;
        blockIn[b_indx].GetComponent<VP_ControlExecute>().execute = true;
        blockIn[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(blockIn[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color.r, blockIn[b_indx].gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
        blockIn[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
    }

    public void Looper(){
        if(b_indx == c_indx){
            if(iterations == 1){
                control.done = true;
                control.execute = false;
                control.DarkColor();
                start.index++;
            }else{
                b_indx = 0;
                StartP();
                iterations--;
            }
        }else{
            StartP();
        }
    }

    void Update()
    {
        if(control.execute){
            if(onStart){
                if(transform.Find("shad_Loop_in").childCount > 0){
                    transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_Loop_in").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
                    transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
                }
                if(transform.Find("shad_Loop_in").childCount > 0){
                    countIn();
                    StartP();
                }

                onStart = false;
            }
        }

        if(control.done){
            Reset();
        }

        if(!start.sim.Playing){
            if(!setter){
                count = true;
                for(int i = 0; i < blockIn.Length; i++){
                    if(blockIn[i] != null){
                        blockIn[i] = null;
                    }
                }
                j = 0;
                c_indx = 0;
                setter = true;
                starter = true;
                onStart = true;
                if(transform.Find("shad_Loop_in").childCount > 0){
                    transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_Loop_in").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
                    transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
                }

                for(int i = 0; i < blockIn.Length; i++){
                    if(blockIn[i] != null){
                        blockIn[i].transform.GetChild(0).GetComponent<Image>().color = new Color(blockIn[i].transform.GetChild(0).GetComponent<Image>().color.r, 
                        blockIn[i].transform.GetChild(0).GetComponent<Image>().color.g, blockIn[i].transform.GetChild(0).GetComponent<Image>().color.b, 1);
                    }
                }
            }
        }
        if(start.sim.Playing){
            if(starter){
                startBlock = transform.Find("shad_Loop_in").gameObject;
                countIn();
                starter = false;
                setter = false;
            }
        }

        if(!control.execute){
            b_indx = 0;
            float.TryParse(tmp_i, out iterations);
            onStart = true;
        }
    }

    void Reset(){
        j = 0;
        onStart = true;
        count = true;
        startBlock = transform.Find("shad_Loop_in").gameObject;
        c_indx = 0;
        b_indx = 0;

        if(transform.Find("shad_Loop_in").childCount > 0){
            transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_Loop_in").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
            transform.Find("shad_Loop_in").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 0.5f);
        }

        for(int i = 0; i < blockIn.Length; i++){
            if(blockIn[i] != null){
                blockIn[i].transform.GetChild(0).GetComponent<Image>().color = new Color(blockIn[i].transform.GetChild(0).GetComponent<Image>().color.r, 
                blockIn[i].transform.GetChild(0).GetComponent<Image>().color.g, blockIn[i].transform.GetChild(0).GetComponent<Image>().color.b, 0.5f);
            }
        }
    }

    public void SetValuesIter(){
        tmp_i = nm_i.text;
        bool enter = false;

        if(tmp_i != ""){
            if(tmp_i == "i"){
                tmp_i = "1101001";
                nm_i.text = "i";
                enter = true;
            }else{
                for(int i = 0; i <= choices_lp.Length-1; i++){
                    if(tmp_i == choices_lp[i]){
                        tmp_i = nm_i.text;
                        enter = true;
                    }
                }

            }   
        }

        if(!enter){
            tmp_i = "1";
            nm_i.text = "1";
        }

        float.TryParse(tmp_i, out iterations);
    }
}
