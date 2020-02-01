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
    public delegate void OnInputHandler(int fromDeviceId, AirConsoleInput input);
    public delegate void OnReadyHandler(string joinCode);
    public delegate void OnConnectHandler(int connectedDeviceId);
    public delegate void OnDisconnectedHandler(int disconnectedDeviceId);

    private OnMessageHandler onMessageHandler = null;
    private OnInputHandler onInputHandler = null;
    private OnReadyHandler onReadyHandler = null;
    private OnConnectHandler onConnectHandler = null;
    private OnDisconnectedHandler onDisconnectHandler = null;

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
        if(data["element"] != null)
        {
            AirConsoleInput input = data.ToObject<AirConsoleInput>();
            this.onInputHandler(fromDeviceId, input);
        }

        else
        {
            AirConsoleMessage message = data.ToObject<AirConsoleMessage>();
            this.onMessageHandler(fromDeviceId, message);
        }
    }

    private void OnConnect(int connectedDeviceId)
    {
        //this.CustomDebug("c: " + connectedDeviceId.ToString());
        this.onConnectHandler(connectedDeviceId);
    }

    private void OnDisconnect(int disconnectedDeviceId)
    {
        this.onDisconnectHandler(disconnectedDeviceId);
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

    public void RegisterEvents(OnReadyHandler onReadyHandler, OnConnectHandler onConnectHandler, OnDisconnectedHandler onDisconnectHandler, OnMessageHandler onMessageHandler, OnInputHandler onInputHandler)
    {
        this.onInputHandler = onInputHandler;
        this.RegisterEvents(onReadyHandler, onConnectHandler, onDisconnectHandler, onMessageHandler);
    }

    public void RegisterEvents(OnReadyHandler onReadyHandler, OnConnectHandler onConnectHandler, OnDisconnectedHandler onDisconnectHandler, OnMessageHandler onMessageHandler)
    {
        this.onReadyHandler = onReadyHandler;
        this.onConnectHandler = onConnectHandler;
        this.onDisconnectHandler = onDisconnectHandler;
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
        this.onInputHandler = null;

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

/*
AirConsoleInput input = data.ToObject<AirConsoleInput>();
if(input != null && !string.IsNullOrEmpty(input.element))
{
    this.onInputHandler(fromDeviceId, input);
}
*/
