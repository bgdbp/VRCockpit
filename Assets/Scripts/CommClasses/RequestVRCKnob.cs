
using NUnit.Framework;
using System.Collections.Generic;

public class RequestVRCKnob : RequestVRCControl
{
    public float Value { get; set; }

    public RequestVRCKnob(string controlID, float value) : base(controlID)
    {
        Value = value;
    }
}