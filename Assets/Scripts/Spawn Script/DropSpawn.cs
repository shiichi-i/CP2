using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSpawn : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform partsPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(partsPanel, Input.mousePosition))
        {
            Debug.Log("spawned");
        }
    }
}
