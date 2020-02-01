using NDream.AirConsole;
using UnityEngine;
using TMPro;

public class GameServer : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    private int numberOfConnectedClients = 0;

    void Awake ()
    {
        AirConsoleMessenger.Instance.RegisterEvents(this.OnReady, this.OnConnect, this.OnDisconnect, this.OnMessage, this.OnInput);
    }

    public void OnReady(string joinCode)
    {
        
    }

    public void OnConnect(int connectedDeviceId)
    {
        this.numberOfConnectedClients++;

        AirConsole.instance.SetActivePlayers(3);
        new AirConsoleMessage(connectedDeviceId, "setDeviceNumber")
            .AddPayload("deviceNumber", AirConsole.instance.ConvertDeviceIdToPlayerNumber(connectedDeviceId).ToString())
            .Send();
    }

    public void OnDisconnect(int disconnectedDevideId)
    {
        this.numberOfConnectedClients--;
    }

    public void OnInput(int fromDeviceId, AirConsoleInput input)
    {
        int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(fromDeviceId);

        if (input.element.Equals("dpad") && input.data.ContainsKey("key") && input.data.ContainsKey("pressed"))
        {
            if (input.data["pressed"].Equals("True"))
            {
                if (input.data["key"].Equals("up"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " up pressed";
                }

                else if (input.data["key"].Equals("down"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " down pressed";
                }

                else if (input.data["key"].Equals("left"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " left pressed";
                }

                else if (input.data["key"].Equals("right"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " right pressed";
                }
            }

            else
            {
                if (input.data["key"].Equals("up"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " up released";
                }

                else if (input.data["key"].Equals("down"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " down released";
                }

                else if (input.data["key"].Equals("left"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " left released";
                }

                else if (input.data["key"].Equals("right"))
                {
                    this.debugText.text = "Player " + playerNumber.ToString() + " right released";
                }
            }
        }

        else if (input.element.Equals("action") && input.data.ContainsKey("pressed"))
        {
            if (input.data["pressed"].Equals("True"))
            {
                this.debugText.text = "Player " + playerNumber.ToString() + " action pressed";
            }

            else
            {
                this.debugText.text = "Player " + playerNumber.ToString() + " action released";
            }
        }
    }

    public void OnMessage(int fromDeviceId, AirConsoleMessage message)
    {

    }

    void OnDestroy()
    {
        AirConsoleMessenger.Instance.UnregisterEvents();
    }
}

/*
if (message.type == "something")
{

    new AirConsoleMessage(AirConsoleMessenger.BroadcastId, "something")
            .AddPayload("something")
            .Send();

    // if(message.PayloadHasValue("something"))
    // { }
}
*/
