using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWindow : MonoBehaviour
{
    public GameObject panel;
    public GameObject arrow;
    public float fOpen, fClose;
    public bool bottom, isOpen;
    public void ToggleTab()
    {
        if (bottom)
        {
            if (isOpen)
            {
                LeanTween.rotateZ(arrow, 0f, 0.3f);
                LeanTween.moveLocalY(panel, fClose, 0.3f);
                isOpen = false;
            }
            else if (!isOpen)
            {
                LeanTween.rotateZ(arrow, 180f, 0.3f);
                LeanTween.moveLocalY(panel, fOpen, 0.3f);
                isOpen = true;
            }

        }

        else if (!bottom)
        {
            if (isOpen)
            {
                LeanTween.rotateY(arrow, 180f, 0.3f);
                LeanTween.moveLocalX(panel, fClose, 0.3f);
                isOpen = false;
            }
            else if (!isOpen)
            {
                LeanTween.rotateY(arrow, 0f, 0.3f);
                LeanTween.moveLocalX(panel, fOpen, 0.3f);
                isOpen = true;
            }
        }
    }
}
