using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VRTemplate;
using System.Threading.Tasks;

public class VRCKnob : VRCControl
{
    public XRKnob Knob;
    public Action<float> OnValueChanged;

    public override async Task SendStateToServer()
    {
        RequestVRCKnob rKnob = new (ControlID, value: Knob.value, isInitialSend);
        await VRCSocketClient.Instance.SendRequest(rKnob);

        isInitialSend = false;
    }


    public async Task _setState(float knobValue, bool sendRequest)
    {
        Knob.SetValueNoNotify(knobValue);

        if (sendRequest)
        {
            await SendStateToServer();
        }
           
        OnValueChanged?.Invoke(Knob.value);
        refreshKnob();

        Debug.Log($"Knob {ControlID} changed!  Its value is {Knob.value}");
    }

    public async Task SetState(float knobValue)
    {
        await _setState(knobValue, sendRequest: false);
    }

    public async Task SetStateAndSync(float knobValue)
    {
        await _setState(knobValue, sendRequest: true);
    }

    bool isFirstTwist = true;
    public async void Twist()
    {
        if (isFirstTwist)
        {
            isFirstTwist = false;
            return;
        }

        if (VRCSocketClient.Instance == null)
            return;

        await SetStateAndSync(Knob.value);
    }

    private void refreshKnob()
    {
        DisplayText.text = $"{ControlID}: {Knob.value:0.00}";
    }

    public override void Start()
    {
        base.Start();
        refreshKnob();
    }
}
