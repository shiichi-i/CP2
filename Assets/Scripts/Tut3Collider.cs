using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut3Collider : MonoBehaviour
{
    public GameObject tutorial;

    void OnTriggerStay(Collider other){
        if(tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 4){
            tutorial.transform.GetChild(0).GetComponent<TutorialManager>().NextTut();
            tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
        }
    }
}
