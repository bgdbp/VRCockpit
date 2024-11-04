using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Text.Json;
using Unity.Collections.LowLevel.Unsafe;

public class VRCSocketClient : MonoBehaviour
{
    public static VRCSocketClient Instance;
    Socket client;

    private async void OnEnable()
    {
        await ConnectToServer();
        Instance = this;
    }

    private void OnDisable()
    {
        client.Close();
    }

    public async Task SendRequest(VRCRequest request)
    {
        string message = JsonSerializer.Serialize(request);

        byte[] messageBytes = BitConverter.GetBytes(sizeof(int) + message.Length)
            .Concat(Encoding.UTF8.GetBytes(message))
            .ToArray();

        //todo: use BeginSend and EndSend for more throughput
        // this current setup waits for each send to be
        // acknowledged before sending more (safe but slow)
        int bytesSent = await client.SendAsync(messageBytes, SocketFlags.None);
    }

    public async Task ConnectToServer()
	{
        string hostName = Dns.GetHostName();
        IPHostEntry localhost = Dns.GetHostEntry(hostName);
        IPAddress localhostIP = localhost.AddressList[0];
        IPEndPoint ipEndPoint = new (localhostIP, 11000);

        client = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        await client.ConnectAsync(ipEndPoint);

        // test requests to make sure we're able to communicate with the server
        RequestVRCKnob rKnob = new ("TestKnob", 0.123456f);
        await SendRequest(rKnob);

        RequestVRCToggle rToggle = new ("TestToggle", isOn: false);
        await SendRequest(rToggle);

        RequestVRCButton rButton = new ("TestButton", isPressed: true);
        await SendRequest(rButton);
    }
}
