using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    public List<GameObject> robotParts;
    public List<Vector3> robotPos;
    public List<Quaternion> robotRot;
    SimManager sim;

    public GameObject tutorial;

    void Start(){
        sim = GameObject.Find("SimBar").GetComponent<SimManager>();
    }

    public void PreRewind(){
        if(sim.Playing){
            robotPos.Clear();
            robotRot.Clear();
            
            for(int i = 0; i < robotParts.Count; i++){
                robotPos.Add(robotParts[i].transform.position);
                robotRot.Add(robotParts[i].transform.rotation);
            }
        }
    }

    public void PostRewind(){
        if(sim.Playing){
            sim.PlayButton();
        }
        for(int i = 0; i < robotParts.Count; i++){
            robotParts[i].transform.position = robotPos[i];
            robotParts[i].transform.rotation = robotRot[i];
        }   
        if(tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 3){
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().NextTut();
                    tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
        }
    }
}
