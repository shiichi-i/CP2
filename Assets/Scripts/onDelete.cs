using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDelete : MonoBehaviour
{
    ObjSelection select;
    AssignmentControl assign;

    void Start()
    {
        select = GameObject.Find("SimBar").GetComponent<ObjSelection>();
        assign = GameObject.Find("Inspector").GetComponent<AssignmentControl>();
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
                    }
                }

                for(int i = index; i < assign.sensors.Length-1; i++){
                    assign.sensors[i] = assign.sensors[i+1];
                    assign.inTake[i] = assign.inTake[i+1];
                }
                assign.sensors[3] = null;
                assign.inTake[3] = 0;
                assign.sensorCount--;
            }

            else{
            int index = 0;
                for(int i=0; i< assign.motors.Length; i++){
                    if(select.currentObj == assign.motors[i]){
                        assign.outTake[i] = 0;
                        assign.motors[i] = null;
                        index = i;
                    }
                }

                for(int i = index; i < assign.motors.Length-1; i++){
                    assign.motors[i] = assign.motors[i+1];
                    assign.outTake[i] = assign.outTake[i+1];
                }
                assign.motors[3] = null;
                assign.outTake[3] = 0;
                assign.motorCount--;
            }
        }

        GameObject arrows = select.currentObj.transform.parent.gameObject;
        select.currentObj.transform.SetParent(null);
        Destroy(arrows);
        Destroy(select.currentObj);
        
    }
}
