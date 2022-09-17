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

    public void PlayButton()
    {
        Playing = !Playing;
        if (Playing)
        {
            bl1.SetActive(true);
            bl2.SetActive(true);
            bl3.SetActive(true);
            status = "Pause";
            display = "Status: SIMULATING";
        }
        else
        {
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
