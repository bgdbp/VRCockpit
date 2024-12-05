
using System.Threading.Tasks;

public class RequestVRCButton : RequestVRCControl
{
    public bool IsPressed { get; set; }

    public override async Task HandleRequest()
    {
        await base.HandleRequest();

        VRCButton button = VRCControl.GetControlByID(ControlID) as VRCButton;

        if (button != null)
        {
            await button.SetState(IsPressed);
        }
    }

    public RequestVRCButton(string controlID, bool isPressed, bool isInitialSend) : base(controlID, isInitialSend)
    {
        IsPressed = isPressed;
    }
}