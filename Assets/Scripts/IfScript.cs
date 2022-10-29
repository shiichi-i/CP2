using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfScript : MonoBehaviour
{
   VP_ControlExecute control;
    VP_Start start;
    public bool onStart, setter, countT, countF, starter;
    int j =0, k=0;
    bool onEnd = false, ret = false;
    public GameObject[] blockInT;
    public GameObject[] blockInF;
    public int b_indx, c_indxT, c_indxF;


    GameObject startBlockT, startBlockF;


    void Start()
    {
        onStart = true;
        control = this.GetComponent<VP_ControlExecute>();
        start = GameObject.Find("Start").GetComponent<VP_Start>();
        starter = true;
        b_indx = 0;
        startBlockT = transform.Find("shad_ifT").gameObject;
        startBlockF = transform.Find("shad_ifF").gameObject;
    }

    void countInT(){
        if(transform.Find("shad_ifT").childCount > 0){
            while(countT){
                if(j < startBlockT.transform.childCount && startBlockT.transform.GetChild(j).tag == "Player"){
                    
                    startBlockT.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color = new Color(startBlockT.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color.r,
                    startBlockT.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color.g, startBlockT.transform.GetChild(j).GetChild(0).gameObject.GetComponent<Image>().color.b, 0.5f);

                    startBlockT = startBlockT.transform.GetChild(j).gameObject;
                    blockInT[c_indxT] = startBlockT;
                    j = 0;
                    c_indxT++;
                }else if(j == startBlockT.transform.childCount-1){
                    countT = false;
                }else{
                    j++;
                }
            }
        }
    }

    void countInF(){
        if(transform.Find("shad_ifF").childCount > 0){
            while(countF){
                if(k < startBlockF.transform.childCount && startBlockF.transform.GetChild(k).tag == "Player"){
                    
                    startBlockF.transform.GetChild(k).GetChild(0).gameObject.GetComponent<Image>().color = new Color(startBlockF.transform.GetChild(k).GetChild(0).gameObject.GetComponent<Image>().color.r,
                    startBlockF.transform.GetChild(k).GetChild(0).gameObject.GetComponent<Image>().color.g, startBlockF.transform.GetChild(k).GetChild(0).gameObject.GetComponent<Image>().color.b, 0.5f);

                    startBlockF = startBlockF.transform.GetChild(k).gameObject;
                    blockInF[c_indxF] = startBlockF;
                    k = 0;
                    c_indxF++;
                }else if(k == startBlockF.transform.childCount-1){
                    countF = false;
                }else{
                    k++;
                }
            }
        }
    }

    public void StartPT(){
        blockInT[b_indx].GetComponent<VP_ControlExecute>().onStart = true;
        blockInT[b_indx].GetComponent<VP_ControlExecute>().execute = true;
        blockInT[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(blockInT[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color.r, blockInT[b_indx].gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
        blockInT[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
    }

    public void StartPF(){
        blockInF[b_indx].GetComponent<VP_ControlExecute>().onStart = true;
        blockInF[b_indx].GetComponent<VP_ControlExecute>().execute = true;
        blockInF[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(blockInF[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color.r, blockInF[b_indx].gameObject.transform.GetChild(0).GetComponent<Image>().color.g,
        blockInF[b_indx].transform.GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
    }

    void End(){
        control.done = true;
        control.DarkColor();
        control.execute = false;
        start.index++;
        onEnd = false;
    }

    void LateUpdate()
    {
        if(control.execute){
            if(onStart){
                if(transform.Find("shad_ifT").childCount > 0){
                    transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_ifT").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
                    transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 1);

                    countInT();
                }
                if(transform.Find("shad_ifF").childCount > 0){
                    transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_ifF").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
                    transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 1);

                    countInF();
                }

                if(transform.Find("shad_Sensor").childCount > 0){
                    if(transform.Find("shad_Sensor").GetComponentInChildren<Sens_Script>().ret){
                        ret = true;
                        FColor();
                        StartPT();
                    }else{
                        ret = false;
                        TColor();
                        StartPF();
                    }
                }
                onEnd = true;
                onStart = false;
            }

            if(onEnd){
                if(b_indx == c_indxT && ret){
                    End();
                }else if(b_indx == c_indxF && !ret){
                    End();
                }
            }

        }

        if(control.done){
            Reset();
        }

        if(!start.sim.Playing){
            if(!setter){
                countT = true;
                countF = true;
                j = 0;
                c_indxT = 0;
                c_indxF = 0;
                setter = true;
                starter = true;
                onStart = true;

                if(transform.Find("shad_ifT").childCount > 0){
                    transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_ifT").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
                    transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
                }
                if(transform.Find("shad_ifF").childCount > 0){
                    transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_ifF").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
                    transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 1);
                }

                for(int i = 0; i < blockInT.Length; i++){
                    if(blockInT[i] != null){
                        blockInT[i].transform.GetChild(0).GetComponent<Image>().color = new Color(blockInT[i].transform.GetChild(0).GetComponent<Image>().color.r, 
                        blockInT[i].transform.GetChild(0).GetComponent<Image>().color.g, blockInT[i].transform.GetChild(0).GetComponent<Image>().color.b, 1);
                    }
                }
                for(int i = 0; i < blockInF.Length; i++){
                    if(blockInF[i] != null){
                        blockInF[i].transform.GetChild(0).GetComponent<Image>().color = new Color(blockInF[i].transform.GetChild(0).GetComponent<Image>().color.r, 
                        blockInF[i].transform.GetChild(0).GetComponent<Image>().color.g, blockInF[i].transform.GetChild(0).GetComponent<Image>().color.b, 1);
                    }
                }

                for(int i = 0; i < blockInT.Length; i++){
                    if(blockInT[i] != null){
                        blockInT[i] = null;
                    }
                }
                for(int i = 0; i < blockInF.Length; i++){
                    if(blockInF[i] != null){
                        blockInF[i] = null;
                    }
                }
                setter = true;
            }
        }

        if(!control.execute){
            b_indx = 0;
            onStart = true;
        }
    }

    void Reset(){
        j = 0;
        k = 0;
        onStart = true;
        countT = true;
        countF = true;
        startBlockT = transform.Find("shad_ifT").gameObject;
        startBlockF = transform.Find("shad_ifF").gameObject;
        c_indxT = 0;
        c_indxF = 0;
        b_indx = 0;
        setter = false;

        if(transform.Find("shad_ifT").childCount > 0){
            transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_ifT").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
            transform.Find("shad_ifT").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 0.5f);
        }
        if(transform.Find("shad_ifF").childCount > 0){
            transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.r, transform.Find("shad_ifF").GetChild(0).GetChild(0).GetComponent<Image>().color.g,
            transform.Find("shad_ifF").GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color.b, 0.5f);
        }

       TColor();
       FColor();
        
    }

    void FColor(){
        for(int o = 0; o < blockInF.Length; o++){
            if(blockInF[o] != null){
                blockInF[o].transform.GetChild(0).GetComponent<Image>().color = new Color(blockInF[o].transform.GetChild(0).GetComponent<Image>().color.r, 
                blockInF[o].transform.GetChild(0).GetComponent<Image>().color.g, blockInF[o].transform.GetChild(0).GetComponent<Image>().color.b, 0.5f);
            }
        }
    }

    void TColor(){
        for(int i = 0; i < blockInT.Length; i++){
            if(blockInT[i] != null){
                blockInT[i].transform.GetChild(0).GetComponent<Image>().color = new Color(blockInT[i].transform.GetChild(0).GetComponent<Image>().color.r, 
                blockInT[i].transform.GetChild(0).GetComponent<Image>().color.g, blockInT[i].transform.GetChild(0).GetComponent<Image>().color.b, 0.5f);
            }
        }
    }
}
