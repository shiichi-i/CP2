using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabOpen : MonoBehaviour
{
    public string Tab;
    public bool VisualBlocks;
    GameObject PTab, MTab, STab;

    GameObject ITab, OTab, CTab;


    private void Start()
    {
        PTab = GameObject.Find("PartTab");
        MTab = GameObject.Find("MotorTab");
        STab = GameObject.Find("SensorTab");

        ITab = GameObject.Find("InTab");
        OTab = GameObject.Find("OutTab");
        CTab = GameObject.Find("ConTab");
    }

    public void SetTab()
    {
        if (VisualBlocks)
        {
            if (Tab == "1")
            {
                ITab.SetActive(true);
                OTab.SetActive(false);
                CTab.SetActive(false);
            }
            if (Tab == "2")
            {
                ITab.SetActive(false);
                OTab.SetActive(false);
                CTab.SetActive(true);
            }
            if (Tab == "3")
            {
                OTab.SetActive(true);
                ITab.SetActive(false);
                CTab.SetActive(false);
            }
        }
        else
        {
            if (Tab == "1")
            {
                PTab.SetActive(true);
                MTab.SetActive(false);
                STab.SetActive(false);
            }
            if (Tab == "2")
            {
                PTab.SetActive(false);
                MTab.SetActive(true);
                STab.SetActive(false);
            }
            if (Tab == "3")
            {
                PTab.SetActive(false);
                MTab.SetActive(false);
                STab.SetActive(true);
            }
        }
    }
        
}
