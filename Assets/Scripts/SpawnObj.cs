using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public GameObject prefab;
    SpawnManager spawnManager;
    ObjSelection outline;
    RewindManager rewinder;
    SimManager simulation;

    void Start()
    {
        spawnManager = GameObject.Find("SimBar").GetComponent<SpawnManager>();
        outline = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        simulation = GameObject.Find("SimBar").GetComponent<SimManager>();
        rewinder = GameObject.Find("SimBar").GetComponentInChildren<RewindManager>();
    }

    public void PressObj()
    {
        spawnManager.ticked = !spawnManager.ticked;
        if (!simulation.Playing)
        {
            if(outline.checkChild){
                Destroy(outline.currentObj.GetComponent<GreenOutline>());
                outline.checkChild = false;
            }
            if (outline.tempObj != null && spawnManager.ticked && outline.tempObj.transform.parent != null && outline.tempObj.transform.parent.tag == "CodeArea")
            {
                Destroy(outline.tempObj.GetComponent<Outline>());
                GameObject arrow = outline.tempObj.transform.parent.gameObject;
                outline.tempObj.transform.SetParent(null);
                Destroy(arrow);
            }
            if (!spawnManager.willSpawn && spawnManager.ticked && !outline.checkChild)
            {
                GameObject a = Instantiate(prefab) as GameObject;
                a.transform.position = new Vector3(0, -50f, 0);
                spawnManager.prefab = a;
                spawnManager.willSpawn = true;
                outline.moving = true;
                SAVE_manager.Instance.AddItem(a);
                
                rewinder.robotParts.Add(a);
            }
            else
            {
                int indx = 0;
                for(int i = 0; i < rewinder.robotParts.Count; i++){
                    if(rewinder.robotParts[i] == spawnManager.prefab){
                        indx = i;
                    }
                }
                rewinder.robotParts.Remove(rewinder.robotParts[indx]);
                indx = 0;

                SAVE_manager.Instance.RemoveItem(spawnManager.prefab.GetComponent<ObjInfo>().SaveID);
                
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
