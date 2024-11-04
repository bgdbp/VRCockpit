
public class RequestVRCToggle : RequestVRCControl
{
    public bool IsOn { get; set; }

    public RequestVRCToggle(string controlID, bool isOn) : base(controlID)
    {
        IsOn = isOn;
    }
}