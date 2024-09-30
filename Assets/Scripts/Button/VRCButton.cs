using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VRCButton : MonoBehaviour
{
    public TextMeshPro DisplayText;
    public string ButtonID;

    public Action OnPress;

    public void Press()
    {
        Debug.Log($"ButtonID {ButtonID} clicked!");
        OnPress?.Invoke();
    }

    public virtual void Start()
    {
        refreshHighlight();
    }

    private void Update()
    {
        refreshHighlight();
    }

    private void refreshHighlight()
    {
    }
}
