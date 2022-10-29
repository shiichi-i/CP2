using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;
    public AudioSource slide;

    void Start(){
        slide = GameObject.Find("CodeBar").GetComponent<AudioSource>();
    }

    public void ToggleTab()
    {
        Animator animator = panel.GetComponent<Animator>();
        bool isOpen = animator.GetBool("isOpen");
        slide.Play();

    animator.SetBool("isOpen", !isOpen);
    }
}
