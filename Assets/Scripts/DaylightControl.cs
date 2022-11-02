using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightControl : MonoBehaviour
{
    public GameObject floor;
    public Material dayMat, nightMat;
    public bool Day;

    public void ChangeDay(){
        if(!Day){
            floor.GetComponent<Renderer>().material = dayMat;
            Camera.main.backgroundColor = new Color(0.9245283f, 0.9145771f, 0.8082354f, 0);
            Day = true;
            SAVE_manager.Instance.SetDay(true);
        }else{
            floor.GetComponent<Renderer>().material = nightMat;
            Camera.main.backgroundColor = new Color(0.2468257f, 0.2514369f, 0.3018868f, 0);
            SAVE_manager.Instance.SetDay(false);
            Day = false;
        }
    }

}
