using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDelete : MonoBehaviour
{
    ObjSelection select;
    AssignmentControl assign;
    RewindManager rewinder;

    public GameObject tutorial;


    void Start()
    {
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
        rewinder = GameObject.Find("SimBar").GetComponentInChildren<RewindManager>();
    }

    public void clickDelete()
    {
        if(!select.currentObj.GetComponent<ObjInfo>().isPart){
            if(select.currentObj.GetComponent<ObjInfo>().isSensor){
            int index = 0;
                for(int i=0; i< assign.sensors.Length; i++){
                    if(select.currentObj == assign.sensors[i]){
                        assign.inTake[i] = 0;
                        assign.sensors[i] = null;
                        index = i;
                        SAVE_manager.Instance.SetAssignments(false, i, null, 0);
                    }
                }

                for(int i = index; i < assign.sensors.Length-1; i++){
                    assign.sensors[i] = assign.sensors[i+1];
                    assign.inTake[i] = assign.inTake[i+1];
                    SAVE_manager.Instance.SetAssignments(false, i, assign.sensors[i], assign.inTake[i]);
                }
                assign.sensors[3] = null;
                assign.inTake[3] = 0;
                SAVE_manager.Instance.SetAssignments(false, 3, null, 0);
                assign.sensorCount--;
                SAVE_manager.Instance.SetAssignmentsCount(false, assign.sensorCount);
            }

            else{
            int index = 0;
                for(int i=0; i< assign.motors.Length; i++){
                    if(select.currentObj == assign.motors[i]){
                        assign.outTake[i] = 0;
                        assign.motors[i] = null;
                        index = i;
                        SAVE_manager.Instance.SetAssignments(true, i, null, 0);
                    }
                }

                for(int i = index; i < assign.motors.Length-1; i++){
                    assign.motors[i] = assign.motors[i+1];
                    assign.outTake[i] = assign.outTake[i+1];
                    SAVE_manager.Instance.SetAssignments(true, i, assign.motors[i], assign.outTake[i]);
                }
                assign.motors[3] = null;
                assign.outTake[3] = 0;
                SAVE_manager.Instance.SetAssignments(true, 3, null, 0);
                assign.motorCount--;
                SAVE_manager.Instance.SetAssignmentsCount(true, assign.motorCount);
            }
        }

        

        if(select.currentObj.transform.parent.gameObject != null){
            GameObject arrows = select.currentObj.transform.parent.gameObject;
            select.currentObj.transform.SetParent(null);
            Destroy(arrows);
        }
            int indx = 0;
            for(int i = 0; i < rewinder.robotParts.Count; i++){
                if(rewinder.robotParts[i] == select.currentObj){
                    indx = i;
                }
            }
            rewinder.robotParts.Remove(rewinder.robotParts[indx]);
            indx = 0;

        SAVE_manager.Instance.RemoveItem(select.currentObj.GetComponent<ObjInfo>().SaveID);
        Destroy(select.currentObj);

        if(tutorial != null && tutorial.transform.GetChild(0).GetComponent<TutorialManager>().indxx == 4){
            tutorial.transform.GetChild(0).GetComponent<TutorialManager>().ShowPop();
        }
    }
}

