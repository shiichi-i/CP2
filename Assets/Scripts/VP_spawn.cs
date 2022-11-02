using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_spawn : MonoBehaviour
{
    public GameObject prefab, spawnPoint;
    Transform panel;
    VP_manager naming;

    SimManager simulation;

    public GameObject tutorial;

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
                SAVE_manager.Instance.AddBlock(a);
                a.name = a.name + naming.blocks.ToString();
                a.transform.SetParent(panel);
                a.GetComponent<RectTransform>().localPosition = spawnPoint.GetComponent<RectTransform>().localPosition;
                a.GetComponent<RectTransform>().localScale = new Vector3(0.09f, 0.09f, 1f);
                naming.spawnEmpty = false;

                if(tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 1){
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
                }
            }
        }
    }

}
