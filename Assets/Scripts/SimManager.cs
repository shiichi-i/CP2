using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimManager : MonoBehaviour
{
    public bool Playing = true;
    public string status, display;
    public Text disp;
    public GameObject bl1, bl2, bl3;
    AvoidCollision collision;
    ObjSelection selection;
    public Button b_play;
    public GameObject del;

    omMerge merge;
    VP_Start vp;

    void Start()
    {
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        merge = GameObject.Find("ShortCuts").GetComponent<omMerge>();
        vp = GameObject.Find("Start").GetComponent<VP_Start>();
    }
    
    void Update(){
        if(selection.onFindMotor || merge.merging){
            b_play.interactable = false;
        }else{
            b_play.interactable = true;
        }
    }

    public void PlayButton()
    {
        if (!collision.isColliding)
        {
            Playing = !Playing;
            
            if(selection.currentObj != null)
                {
                    selection.play = true;
                }
            
            if (Playing)
            {
                vp.CountBlocks();
                vp.ChangeColor();
                vp.index = 0;
                vp.StartProgram();

                bl1.SetActive(true);
                bl2.SetActive(true);
                bl3.SetActive(true);
                del.SetActive(false);
                display = "Status: SIMULATING";
                b_play.transform.GetChild(0).gameObject.SetActive(false);
                b_play.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                vp.EndProgram();
                vp.index = 0;
                
                bl1.SetActive(false);
                bl2.SetActive(false);
                bl3.SetActive(false);
                del.SetActive(true);
                display = "Status: PAUSED";
                b_play.transform.GetChild(0).gameObject.SetActive(true);
                b_play.transform.GetChild(1).gameObject.SetActive(false);
            }
            disp.text = display;
        }
        
    }
}
