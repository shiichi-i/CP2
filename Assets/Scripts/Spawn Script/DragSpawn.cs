using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSpawn : MonoBehaviour, IDragHandler, IEndDragHandler
{
    SpawnObjClick spawnScript;
    bool isDragging = false;

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }



}
