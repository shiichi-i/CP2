using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;

    public void ToggleTab()
    {
        Animator animator = panel.GetComponent<Animator>();
        bool isOpen = animator.GetBool("isOpen");

    animator.SetBool("isOpen", !isOpen);
    }
}
