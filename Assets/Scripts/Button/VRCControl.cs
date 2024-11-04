using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VRCControl : MonoBehaviour
{
    public TextMeshPro DisplayText;
    public string ControlID;

    public virtual void Start()
    {
        DisplayText.text = ControlID;
    }
}
