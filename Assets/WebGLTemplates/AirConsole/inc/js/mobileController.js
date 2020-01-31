var currentDisplayedScreen = "";
var isDirector = false;
var connectionGranted = false;
var airConsoleMessenger = null;
var votingBlocked = false;
var lastScreenName = "";
var currentScreenName = "";
var debug = false;
var voteSuccessfull = false;

$(function ()
{
    InitTouchEvents();

    airConsoleMessenger = new AirConsoleMessenger();
    airConsoleMessenger.registerEvents(onMessage, onConnectCallback);
    airConsoleMessenger.showDefaultUI(false);
});

function onMessage(fromDeviceId, message)
{
    if (message.type === "something")
    {
        airConsoleMessenger.sendMessage((new AirConsoleMessage(AirConsole.SCREEN, "something")).addPayload("something", "something"));
    }
}

function onConnectCallback(connectedDeviceId)
{
    
}

function InitTouchEvents()
{
    if (!("ontouchstart" in document.createElement("div")))
    {
        var elements = document.getElementsByTagName("*");
        for (var i = 0; i < elements.length; ++i)
        {
            var element = elements[i];

            var ontouchstart = element.getAttribute("ontouchstart");
            if (ontouchstart)
            {
                element.setAttribute("onmousedown", ontouchstart);
            }

            var ontouchend = element.getAttribute("ontouchend");
            if (ontouchend)
            {
                element.setAttribute("onmouseup", ontouchend);
            }
        }
    }
}