using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public CamControls camControls;
    public CodeAreaControls codeControls;

    void LateUpdate()
    {
        if (camControls.onCamStart)
        {
            camControls.HandlePanning();
            camControls.HandleRotating();
            codeControls.onCamStart = true;
            camControls.onUIStart = false;
        }

        if (codeControls.onUIStart)
        {
            codeControls.HandlePanning();
            codeControls.HandleLimits();
            camControls.onUIStart = true;
            camControls.onCamStart = false;
        }

        if (!camControls.onCamStart)
        {
            codeControls.onCamStart = false;
        }
        if (!codeControls.onUIStart)
        {
            camControls.onUIStart = false;
        }

    }
}
