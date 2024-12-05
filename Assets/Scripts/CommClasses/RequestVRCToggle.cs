
using System.Threading.Tasks;

public class RequestVRCToggle : RequestVRCControl
{
    public bool IsOn { get; set; }

    public override async Task HandleRequest()
    {
        await base.HandleRequest();

        VRCToggle toggle = VRCControl.GetControlByID(ControlID) as VRCToggle;

        if (toggle != null)
        {
            await toggle.SetState(IsOn);
        }
    }

    public RequestVRCToggle(string controlID, bool isOn, bool isInitialSend) : base(controlID, isInitialSend)
    {
        IsOn = isOn;
    }
}