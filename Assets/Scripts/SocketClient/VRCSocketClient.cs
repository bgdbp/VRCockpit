using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Text.Json;
using Unity.Collections.LowLevel.Unsafe;
using System.Collections.Generic;

public class VRCSocketClient : MonoBehaviour
{
    public static VRCSocketClient Instance;
    TcpClient client { get; set; }
    IPEndPoint discoveryEndpoint = new (IPAddress.Broadcast, 11000);
    IPEndPoint tcpEndpoint;

    Task connecting;

    public async Task SendRequest(VRCRequest request)
    {
        if (this == null)
            return;

        while (this != null)
        {
            try
            {
                await WaitForConnection();

                var networkStream = client.GetStream();
                string message = JsonSerializer.Serialize(request);
                message += ",";

                byte[] bytes = Encoding.UTF8.GetBytes(message);
                await networkStream.WriteAsync(bytes, 0, bytes.Length);
                return;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }

    private void Start()
    {
    }

    private void Awake()
    {
        Instance = this;
    }



    private async void OnEnable()
    {

        //connect
        //todo: figure out reconnect


        await WaitForConnection();


        foreach (VRCControl control in VRCControl.controlMap.Values)
        {
            await control.SendStateToServer();
        }

        await Receive();
    }

    private void OnDisable()
    {
        closeConnection();
    }

    public async Task Receive()
    {
        try
        {
            var stream = client.GetStream();
            JsonSerializerOptions options = new JsonSerializerOptions() { AllowTrailingCommas = true };
            var incomingRequests = JsonSerializer.DeserializeAsyncEnumerable<VRCRequest>(stream, options);

            await foreach (var incomingRequest in incomingRequests)
            {
                if (incomingRequest != null)
                {
                    await incomingRequest.HandleRequest();
                }
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
        finally
        {
            Console.WriteLine($"Connection closed.");
        }
    }

    private async Task discoverAndConnect()
    {
        if (tcpEndpoint == null)
        {
            await discoverServer();
        }

        await connectToServer();
    }

    private async Task WaitForConnection()
    {
        if (!IsConnected)
        {
            if (connecting == null || connecting.IsFaulted)
            {
                connecting = discoverAndConnect();
            }

            await connecting;
        }

        connecting = null;
    }

    private bool IsConnected
    {
        get
        {
            return client != null && client.Connected;
        }
    }


    async Task discoverServer()
    {
        UdpClient discoveryClient = new();

        Task<UdpReceiveResult> messageReceive = discoveryClient.ReceiveAsync();

        while (tcpEndpoint == null)
        {
            string message = "VRCDiscover";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await discoveryClient.SendAsync(messageBytes, messageBytes.Length, discoveryEndpoint);


            await Task.Run(() => messageReceive.Wait(millisecondsTimeout: 5000), destroyCancellationToken);

            if (!messageReceive.IsCompleted)
            {
                continue;
                
            }

            byte[] receivedMessageBytes = messageReceive.Result.Buffer;
            string receivedMessage = Encoding.UTF8.GetString(receivedMessageBytes);

            string substring = receivedMessage.Substring(0, message.Length);

            if (substring == message)
            {
                string[] parts = receivedMessage.Split("|");
                string addressString = parts[1];
                string portString = parts[2];

                int port = int.Parse(portString);
                IPAddress address = IPAddress.Parse(addressString);

                tcpEndpoint = new IPEndPoint(address, port); 
            }
            Console.WriteLine(receivedMessage);
        }
    }

    async Task connectToServer()
	{
        client = new TcpClient(tcpEndpoint.AddressFamily);
        await client.ConnectAsync(tcpEndpoint.Address, tcpEndpoint.Port);

        byte[] bytes = Encoding.UTF8.GetBytes("[");
        await client.GetStream().WriteAsync(bytes, 0, bytes.Length);
    }

    async void closeConnection()
    {
        string message = "]";

        byte[] bytes = Encoding.UTF8.GetBytes(message);
        await client.GetStream().WriteAsync(bytes, 0, bytes.Length);

        client.LingerState = new LingerOption(true, 0);
        client.Close();
    }
}
