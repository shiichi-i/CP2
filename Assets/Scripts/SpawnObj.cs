using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public GameObject prefab;
    SpawnManager spawnManager;
    ObjSelection outline;

    void Start()
    {
        spawnManager = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();
    }

    public void PressObj()
    {
        if(outline.tempObj != null)
        {
            Destroy(outline.tempObj.GetComponent<Outline>());
        }
        GameObject a = Instantiate(prefab) as GameObject;
        a.transform.position = new Vector3(0, -50f, 0);
        spawnManager.prefab = a;
        spawnManager.willSpawn = true;
    }
}
