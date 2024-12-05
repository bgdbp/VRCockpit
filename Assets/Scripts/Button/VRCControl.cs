using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class VRCControl : MonoBehaviour
{
    public static Dictionary<string, VRCControl> controlMap = new Dictionary<string, VRCControl>();

    public TextMeshPro DisplayText;
    public string ControlID;

    protected bool isInitialSend = true;

    public static VRCControl GetControlByID(string controlID)
    {
        return controlMap[controlID];
    }

    public virtual Task SendStateToServer()
    {
        return Task.CompletedTask;
    }

    public virtual void Start()
    {
        DisplayText.text = ControlID;
        controlMap[ControlID] = this;
    }
}
