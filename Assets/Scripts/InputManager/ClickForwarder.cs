using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickForwarder : MonoBehaviour, InputManager.IClickable3D
{
    public Transform EventTarget;

    InputManager.IClickable3D[] eventTargetInterfaces;

    private void Start()
    {
        eventTargetInterfaces = EventTarget.GetComponents<InputManager.IClickable3D>();
    }

    public void Click(InputManager.InputRaycastResult clickInfo)
    {
        foreach (var target in eventTargetInterfaces)
        {
            target.Click(clickInfo);
        }
    }

    public void Hover(InputManager.InputRaycastResult hoverInfo)
    {
        if (eventTargetInterfaces == null)
            return;

        foreach (var target in eventTargetInterfaces)
        {
            target.Hover(hoverInfo);
        }
    }
}
