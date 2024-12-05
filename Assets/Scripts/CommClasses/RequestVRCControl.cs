
using System;
using System.Threading.Tasks;

public class RequestVRCControl : VRCRequest
{
    public string ControlID { get; set; }
    public bool IsInitialSend { get; set; }

    public override async Task HandleRequest()
    {
        Console.WriteLine($"Received message from {ControlID}");
        await Task.CompletedTask;
    }

    public RequestVRCControl(string controlID, bool isInitialSend)
    {
        ControlID = controlID;
        IsInitialSend = isInitialSend;
    }
}