using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public GameObject prefab;
    SpawnManager spawnManager;
    ObjSelection outline;

    SimManager simulation;

    void Start()
    {
        spawnManager = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        simulation = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    public void PressObj()
    {
        spawnManager.ticked = !spawnManager.ticked;

        if (!simulation.Playing)
        {
            if (outline.tempObj != null && spawnManager.ticked)
            {
                Destroy(outline.tempObj.GetComponent<Outline>());
                GameObject arrow = outline.tempObj.transform.parent.gameObject;
                outline.tempObj.transform.SetParent(null);
                Destroy(arrow);
            }
            if (!spawnManager.willSpawn && spawnManager.ticked)
            {
                GameObject a = Instantiate(prefab) as GameObject;
                a.transform.position = new Vector3(0, -50f, 0);
                spawnManager.prefab = a;
                spawnManager.willSpawn = true;
                outline.moving = true;
            }
            else
            {
                Destroy(spawnManager.prefab);
                spawnManager.prefab = null;
                outline.currentObj = null;
                outline.tempObj = null;
                outline.moving = false;
                spawnManager.willSpawn = false;
            }
        }

    }
}
