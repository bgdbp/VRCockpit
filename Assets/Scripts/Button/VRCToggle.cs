using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class VRCToggle : VRCControl
{
    public GameObject ToggleUpModel;
    public GameObject ToggleDownModel;
    public TextMeshPro ToggleText;
    public bool ToggleState;

    public Action<bool> OnToggle;

    string displayName;

    public override async Task SendStateToServer()
    {
        RequestVRCToggle rToggle = new (ControlID, isOn: ToggleState, isInitialSend);
        await VRCSocketClient.Instance.SendRequest(rToggle);

        isInitialSend = false;
    }

    public async Task _setState(bool toggleState, bool sendRequest)
    {
        ToggleState = toggleState;

        if (sendRequest)
        {
            await SendStateToServer();
        }

        refreshToggleModel();
        OnToggle?.Invoke(ToggleState);

        Debug.Log($"Toggle {ControlID} clicked!  Its state is {ToggleState}");
    }

    public async Task SetState(bool toggleState)
    {
        await _setState(toggleState, sendRequest: false);
    }

    public async Task SetStateAndSync(bool toggleState)
    {
        await _setState(toggleState, sendRequest: true);
    }

    public async void Interact()
    {
        await SetStateAndSync(!ToggleState);
    }

    // in a more complex setup we could use an animation instead
    // of enabling 'up' and 'down' objects to show the toggle states
    private void refreshToggleModel()
    {
        ToggleUpModel.SetActive(ToggleState);
        ToggleDownModel.SetActive(!ToggleState);

        string toggleStateText = ToggleState ? "On" : "Off";
        ToggleText.text = $"{displayName}: {toggleStateText}";
    }

    public override void Start()
    {
        displayName = ToggleText.text;
        refreshToggleModel();
        base.Start();
    }
}
