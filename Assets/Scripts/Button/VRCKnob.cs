using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.UIElements;

public class VRCKnob : VRCButton
{
    public XRKnob Knob;
    public Action<float> OnValueChanged;

    public void Twist()
    {
        OnValueChanged?.Invoke(Knob.value);
        refreshKnob();
    }

    private void refreshKnob()
    {
        DisplayText.text = $"{ButtonID}: {Knob.value:0.00}";
    }

    public override void Start()
    {
        base.Start();

        refreshKnob();
    }
}
