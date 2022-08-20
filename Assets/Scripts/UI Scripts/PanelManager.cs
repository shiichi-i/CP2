using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    ToggleWindow coding;
    ToggleWindow building;
    public Button codeButton;
    public Button buildButton;
    void Start()
    {
        coding = GameObject.Find("BlockBar").GetComponent<ToggleWindow>();
        building = GameObject.Find("BuildBar").GetComponent<ToggleWindow>();
        buildButton.onClick.AddListener(onBuildClick);
        codeButton.onClick.AddListener(onCodeClick);
    }

    // Update is called once per frame
    void onBuildClick()
    {
        if (building.isOpen && !coding.isOpen)
        {
            coding.ToggleTab();
        }
    }

    void onCodeClick()
    {
        if (building.isOpen && !coding.isOpen)
        {
            building.ToggleTab();
        }
    }
}
