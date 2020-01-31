using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleMessenger
{
    public const int BroadcastId = -1;

    public delegate void OnMessageHandler(int fromDeviceId, AirConsoleMessage message);
    public delegate void OnReadyHandler(string joinCode);
    public delegate void OnConnectHandler(int connectedDeviceId);

    private OnMessageHandler onMessageHandler = null;
    private OnReadyHandler onReadyHandler = null;
    private OnConnectHandler onConnectHandler = null;

    private static AirConsoleMessenger instance = null;
    
    private AirConsoleMessenger() { }

    ~AirConsoleMessenger()
    {
        this.UnregisterEvents();
    }

    public static AirConsoleMessenger Instance
    {
        get
        {
            if (AirConsoleMessenger.instance == null)
            {
                AirConsoleMessenger.instance = new AirConsoleMessenger();
            }

            return AirConsoleMessenger.instance;
        }
    }

    private void OnMessage(int fromDeviceId, JToken data)
    {
        AirConsoleMessage message = data.ToObject<AirConsoleMessage>();
        this.onMessageHandler(fromDeviceId, message);
    }

    private void OnConnect(int connectedDeviceId)
    {
        //this.CustomDebug("c: " + connectedDeviceId.ToString());
        this.onConnectHandler(connectedDeviceId);
    }

    private void OnDisconnect(int disconnectedDeviceId)
    {
        //this.CustomDebug("d: " + disconnectedDeviceId.ToString());
    }

    private void OnReady(string joinCode)
    {
        this.ShowDefaultUI(false);
        this.onReadyHandler(joinCode);
    }

    public void SendMessage(AirConsoleMessage message)
    {
        if(message.toDeviceId < 0)
        {
            AirConsole.instance.Broadcast(message);
        }

        else
        {
            AirConsole.instance.Message(message.toDeviceId, message);
        }
    }

    public void ShowDefaultUI(bool showDefaultUI)
    {
        AirConsole.instance.ShowDefaultUI(showDefaultUI);
    }

    public void RegisterEvents(OnReadyHandler onReadyHandler, OnConnectHandler onConnectHandler, OnMessageHandler onMessageHandler)
    {
        this.onReadyHandler = onReadyHandler;
        this.onConnectHandler = onConnectHandler;
        this.onMessageHandler = onMessageHandler;

        AirConsole.instance.onReady += this.OnReady;
        AirConsole.instance.onConnect += this.OnConnect;
        AirConsole.instance.onMessage += this.OnMessage;
        AirConsole.instance.onDisconnect += this.OnDisconnect;
    }

    public void UnregisterEvents()
    {
        this.onReadyHandler = null;
        this.onConnectHandler = null;
        this.onMessageHandler = null;

        // unregister events
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onReady -= this.OnReady;
            AirConsole.instance.onConnect -= this.OnConnect;
            AirConsole.instance.onMessage -= this.OnMessage;
            AirConsole.instance.onDisconnect -= this.OnDisconnect;
        }
    }
}
