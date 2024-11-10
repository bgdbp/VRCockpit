using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VRTemplate;

public class VRCKnob : VRCControl
{
    public XRKnob Knob;
    public Action<float> OnValueChanged;

    public async void Twist()
    {
        if (VRCSocketClient.Instance == null)
            return;

        RequestVRCKnob rKnob = new (ControlID, Knob.value);

        await VRCSocketClient.Instance.SendRequest(rKnob);

        OnValueChanged?.Invoke(Knob.value);
        refreshKnob();
    }

    private void refreshKnob()
    {
        DisplayText.text = $"{ControlID}: {Knob.value:0.00}";
    }

    public override void Start()
    {
        refreshKnob();
    }
}
