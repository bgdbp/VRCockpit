
public class RequestVRCControl : VRCRequest
{
    public string ControlID { get; set; }

    public RequestVRCControl(string controlID)
    {
        ControlID = controlID;
    }
}