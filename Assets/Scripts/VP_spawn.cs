using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VP_spawn : MonoBehaviour
{
    public GameObject prefab, spawnPoint;
    Transform panel;
    VP_manager naming;

    SimManager simulation;

    void Start()
    {
        panel = GameObject.Find("Panel").GetComponent<Transform>();
        naming = GameObject.Find("CodeArea").GetComponent<VP_manager>();
        simulation = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    public void SpawnBlock()
    {
        if (!simulation.Playing)
        {
            if (naming.spawnEmpty)
            {
                naming.blocks++;
                GameObject a = Instantiate(prefab) as GameObject;
                a.name = a.name + naming.blocks.ToString();
                a.transform.SetParent(panel);
                a.GetComponent<RectTransform>().localPosition = spawnPoint.GetComponent<RectTransform>().localPosition;
                a.GetComponent<RectTransform>().localScale = spawnPoint.GetComponent<RectTransform>().localScale;
            }
        }
    }

}
