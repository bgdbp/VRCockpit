using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VRCButton : VRCControl
{
    public Action OnPress;

    public async void Press()
    {
        Debug.Log($"ButtonID {ControlID} clicked!");

        RequestVRCButton rButton = new (ControlID, isPressed: true);
        await VRCSocketClient.Instance.SendRequest(rButton);

        OnPress?.Invoke();
    }

    public override void Start()
    {
        base.Start();
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
