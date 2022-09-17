using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public GameObject prefab;
    SpawnManager spawnManager;
    ObjSelection outline;
    public bool ticked;

    SimManager simulation;

    void Start()
    {
        spawnManager = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        simulation = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    public void PressObj()
    {
        if (!simulation.Playing)
        {
            if (outline.tempObj != null)
            {
                Destroy(outline.tempObj.GetComponent<Outline>());
            }
            /*if (spawnManager.notPlaced)
            {
                Destroy(spawnManager.prefab);
                spawnManager.prefab = null;
                outline.currentObj = null;
                spawnManager.willSpawn = false;
                spawnManager.notPlaced = false;
            }*/

            if (!spawnManager.willSpawn)
            {
                ticked = true;
                GameObject a = Instantiate(prefab) as GameObject;
                a.transform.position = new Vector3(0, -50f, 0);
                spawnManager.prefab = a;
                spawnManager.willSpawn = true;
            }
        }

    }
}
