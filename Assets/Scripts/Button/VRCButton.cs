using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class VRCButton : VRCControl
{
    public GameObject PressedModel;
    public GameObject UnpressedModel;
    public TextMeshPro ButtonText;

    private bool isPressed;

    public override async Task SendStateToServer()
    {
        RequestVRCButton rButton = new (ControlID, isPressed: isPressed, isInitialSend);
        await VRCSocketClient.Instance.SendRequest(rButton);

        isInitialSend = false;
    }

    async Task _setState(bool isPressed, bool sendRequest)
    {
        this.isPressed = isPressed;

        if (sendRequest)
        {
            await SendStateToServer();
        }

        PressedModel.SetActive(isPressed);
        UnpressedModel.SetActive(!isPressed);
        refreshText();
        Debug.Log($"Button {ControlID} clicked!  Its value is {isPressed}");
    }


    public async Task SetState(bool isPressed)
    {
        await _setState(isPressed, sendRequest: false);
    }

    public async Task SetStateAndSync(bool isPressed)
    {
        await _setState(isPressed, sendRequest: true);
    }

    private void refreshText()
    {
        if (isPressed)
            ButtonText.text = "Button: On";
        else
            ButtonText.text = "Button: Off";
    }

    public async void Press()
    {
        await SetStateAndSync(isPressed: true);

    }
    public async void UnPress()
    {
        await SetStateAndSync(isPressed: false);
    }

    public override void Start()
    {
        base.Start();
    }
}
