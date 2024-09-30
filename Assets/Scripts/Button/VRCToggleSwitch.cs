using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VRCToggleSwitch : VRCButton
{
    public GameObject ToggleUpModel;
    public GameObject ToggleDownModel;
    public TextMeshPro ToggleText;
    public bool ToggleState;

    public Action<bool> OnToggle;

    string displayName;

    public void Interact()
    {
        ToggleState = !ToggleState;

        refreshToggleModel();
        OnToggle?.Invoke(ToggleState);

        Debug.Log($"Toggle {ButtonID} clicked!  Its state is {ToggleState}");
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
        base.Start();

        displayName = ToggleText.text;
        refreshToggleModel();
    }
}
