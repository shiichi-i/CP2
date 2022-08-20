using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWindow : MonoBehaviour
{
    public GameObject panel;
    public GameObject arrow;
    public bool isOpen = true;
    public void CloseTab()
    {
        if (isOpen)
        {
            LeanTween.rotateY(arrow, 0f, 0.3f);
            LeanTween.moveX(panel, 650f, 0.3f);
            isOpen = false;
        }
        else if (!isOpen)
        {
            LeanTween.rotateY(arrow, 180f, 0.3f);
            LeanTween.moveX(panel, 400f, 0.3f);
            isOpen = true;
        }
    }
}
