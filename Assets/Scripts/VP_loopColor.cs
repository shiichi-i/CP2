using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VP_loopColor : MonoBehaviour
{
    Transform elParent;

    void Start()
    {
        elParent = this.transform.parent.GetChild(0);
    }

    void Update()
    {
        this.GetComponent<Image>().color = elParent.GetComponent<Image>().color;
    }
}
