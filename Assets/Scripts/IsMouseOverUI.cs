using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsMouseOverUI : MonoBehaviour
{
    public bool IsMouseOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
