using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafScript : MonoBehaviour
{
    RectTransform leaf;
    public float rot;

    void Start()
    {
        leaf = GetComponent<RectTransform>();
        rot = Random.Range(1, 10);
    }

    void Update()
    {
        if(rot > 5)
        leaf.eulerAngles = new Vector3(leaf.eulerAngles.x, leaf.eulerAngles.y, leaf.eulerAngles.z + 0.3f);

        else
        leaf.eulerAngles = new Vector3(leaf.eulerAngles.x, leaf.eulerAngles.y, leaf.eulerAngles.z - 0.3f);
    }
}
