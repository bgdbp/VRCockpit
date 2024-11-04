
public class RequestVRCButton : RequestVRCControl
{
    public bool IsPressed { get; set; }

    public RequestVRCButton(string controlID, bool isPressed) : base(controlID)
    {
        IsPressed = isPressed;
    }
}