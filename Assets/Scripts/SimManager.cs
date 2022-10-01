using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimManager : MonoBehaviour
{
    public bool Playing = true;
    public string status, display;
    public Text test, disp;
    public GameObject bl1, bl2, bl3;
    AvoidCollision collision;
    ObjSelection selection;
    public Button b_play;

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
            if (Playing)
            {
                if(selection.currentObj != null)
                {
                    selection.play = true;
                }
                bl1.SetActive(true);
                bl2.SetActive(true);
                bl3.SetActive(true);
                status = "Pause";
                display = "Status: SIMULATING";
                vp.CountBlocks();
            }
            else
            {
                if (selection.currentObj != null)
                {
                    selection.play = true;
                }
                bl1.SetActive(false);
                bl2.SetActive(false);
                bl3.SetActive(false);
                status = "Play";
                display = "Status: PAUSED";
            }
            test.text = status;
            disp.text = display;
        }
        
    }
}
