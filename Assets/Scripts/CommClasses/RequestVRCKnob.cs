
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RequestVRCKnob : RequestVRCControl
{
    public float Value { get; set; }

    public override async Task HandleRequest()
    {
        await base.HandleRequest();

        VRCKnob knob = VRCControl.GetControlByID(ControlID) as VRCKnob;

        if (knob != null)
        {
            await knob.SetState(Value);
        }
    }

    public RequestVRCKnob(string controlID, float value, bool isInitialSend) : base(controlID, isInitialSend)
    {
        Value = value;
    }
}