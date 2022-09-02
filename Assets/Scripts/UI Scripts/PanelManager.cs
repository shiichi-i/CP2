using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject coding;
    public GameObject building;

    public void onBuildClick()
    {
        if(building.GetComponent<Animator>().GetBool("isOpen") && !coding.GetComponent<Animator>().GetBool("isOpen"))
        {
            coding.GetComponent<Animator>().SetBool("isOpen", true);
        }
    }

    public void OnCodeClick()
    {
        if (building.GetComponent<Animator>().GetBool("isOpen") && !coding.GetComponent<Animator>().GetBool("isOpen"))
        {
            building.GetComponent<Animator>().SetBool("isOpen", false);
        }
    }

}
