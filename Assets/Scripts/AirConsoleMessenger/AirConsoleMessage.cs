using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;

public class AirConsoleMessage
{
    public int fromDeviceId = 0;
    public int toDeviceId = 0;

    public string type = "";
    public Dictionary<string, string> payload = null;

    public AirConsoleMessage(int toDeviceId, string type, Dictionary<string, string> payload)
    {
        this.fromDeviceId = AirConsole.instance.GetDeviceId();
        this.toDeviceId = toDeviceId;
        this.type = type;
        this.payload = payload;
    }

    public AirConsoleMessage(int toDeviceId, string type)
    {
        this.fromDeviceId = AirConsole.instance.GetDeviceId();
        this.toDeviceId = toDeviceId;
        this.type = type;
        this.payload = new Dictionary<string, string>();
    }

    public AirConsoleMessage()
    {
        this.fromDeviceId = AirConsole.instance.GetDeviceId();
        this.payload = new Dictionary<string, string>();
    }

    public void Send()
    {
        AirConsoleMessenger.Instance.SendMessage(this);
    }

    public AirConsoleMessage AddPayload(string key, string value)
    {
        this.payload.Add(key, value);

        return this;
    }

    public AirConsoleMessage AddPayload(string key)
    {
        this.AddPayload(key, "");
        return this;
    }

    public bool PayloadExists(string key)
    {
        if(this.payload.ContainsKey(key))
        {
            return true;
        }

        return false;
    }

    public bool PayloadHasValue(string key)
    {
        if (this.PayloadExists(key) && !string.IsNullOrEmpty(this.payload[key]))
        {
            return true;
        }

        return false;
    }
}