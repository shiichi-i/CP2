using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeManager : MonoBehaviour
{
    public GameObject lvl1, lvl2, lvl3, lvl4;

    void Start(){
        if(StateController.level1){
            lvl1.SetActive(true);
        }

        if(StateController.level2){
            lvl2.SetActive(true);
        }

        if(StateController.level3){
            lvl3.SetActive(true);
        }

        if(StateController.level4){
            lvl4.SetActive(true);
        }
    }
}
