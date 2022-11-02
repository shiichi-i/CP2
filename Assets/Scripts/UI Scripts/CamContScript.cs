using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamContScript : MonoBehaviour
{
    public GameObject camcont;
    
    public void ShowC(){
        camcont.SetActive(true);
    }

    public void HideC(){
        camcont.SetActive(false);
    }
}
