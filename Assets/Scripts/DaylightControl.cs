using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightControl : MonoBehaviour
{
    public GameObject floor, dirLight;
    public Material dayMat, nightMat;
    public bool Day;

    public AudioSource daySound, nightSound;

    Vector3 dayPos, nightPos;

    void Start(){
        dayPos = new Vector3(60f, 0, 0);
        nightPos = new Vector3(220f, 0, 0);
    }

    public void ChangeDay(){
        if(!Day){
            floor.GetComponent<Renderer>().material = dayMat;
            Camera.main.backgroundColor = new Color(0.9245283f, 0.9145771f, 0.8082354f, 0);
            dirLight.transform.eulerAngles = Vector3.Lerp(dirLight.transform.eulerAngles, dayPos, 1.5f);
            daySound.Play();
            Day = true;
            SAVE_manager.Instance.SetDay(true);
        }else{
            floor.GetComponent<Renderer>().material = nightMat;
            Camera.main.backgroundColor = new Color(0.2468257f, 0.2514369f, 0.3018868f, 0);
            dirLight.transform.eulerAngles = Vector3.Lerp(dirLight.transform.eulerAngles, nightPos, 1.5f);
            nightSound.Play();
            SAVE_manager.Instance.SetDay(false);
            Day = false;
        }
    }

}
