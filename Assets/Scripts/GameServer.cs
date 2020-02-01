using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameServer : MonoBehaviour
{
    void Awake ()
    {
        AirConsoleMessenger.Instance.RegisterEvents(this.OnReady, this.OnConnect, this.OnMessage, this.OnInput);
    }

    public void OnReady(string joinCode)
    {
        
    }

    public void OnConnect(int connectedDeviceId)
    {

    }

    public void OnMessage(int fromDeviceId, AirConsoleMessage message)
    {
        if (message.type == "something")
        {
            new AirConsoleMessage(AirConsoleMessenger.BroadcastId, "unsetCockies")
                    .AddPayload("unsetCockies")
                    .Send();

            // if(message.PayloadHasValue("something"))
            // { }
        }
    }

    public void OnInput(int fromDeviceId, AirConsoleInput input)
    {
        Debug.Log(input.data["pressed"].ToString());
    }

    private void HandleConnection(int deviceId, bool connectionGranted)
    {

    }

    void OnDestroy()
    {
        AirConsoleMessenger.Instance.UnregisterEvents();
    }
}