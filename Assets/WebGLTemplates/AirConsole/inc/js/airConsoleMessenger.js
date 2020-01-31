var airconsole = null;

// AirConsoleMessage //
function AirConsoleMessage(toDeviceId = AirConsole.SCREEN, type = "", payload = {})
{
    this.fromDeviceId = airconsole.getDeviceId();
    this.toDeviceId = toDeviceId;
    this.type = type;
    this.payload = payload;
}

AirConsoleMessage.prototype.addPayload = function (key, value = "")
{
    this.payload[key] = value;
    return this;
};

// END AirConsoleMessage //

// AirConsoleMessenger //
function AirConsoleMessenger(orientation)
{
    this.onMessageHandler = null;
    this.BroadcastId = -1;
    airconsole = new AirConsole({ "orientation": orientation });
}

AirConsoleMessenger.prototype.registerEvents = function (onMessageCallback, onConnectCallback = null)
{
    if (onConnectCallback !== null)
    {
        airconsole.onConnect = onConnectCallback;
    }

    airconsole.onMessage = function (fromDeviceId, message)
    {
        // give message some methods to check own payload
        // neccessary because it do not inherit from message above
        // but is created out of json directly
        message.payloadExists = function (key)
        {
            if (this.payload.hasOwnProperty(key))
            {
                return true;
            }
            return false;
        };

        message.payloadHasValue = function (key)
        {
            if (this.payloadExists(key) && this.payload[key] && this.payload[key] !== "")
            {
                return true;
            }

            return false;
        };

        onMessageCallback(fromDeviceId, message);
    };
};

AirConsoleMessenger.prototype.sendMessage = function (message)
{
    if (message.toDeviceId < 0)
    {
        airconsole.broadcast(message);
    }

    else
    {
        airconsole.message(message.toDeviceId, message);
    }
};

AirConsoleMessenger.prototype.showDefaultUI = function (showDefaultUI)
{
    airconsole.showDefaultUI(showDefaultUI);
};

// END AirConsoleMessenger //