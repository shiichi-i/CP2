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

    void Start()
    {
        collision = GameObject.Find("SimBar").GetComponent<AvoidCollision>();
        selection = GameObject.Find("SimBar").GetComponent<ObjSelection>();
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
