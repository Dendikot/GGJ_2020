$(function ()
{
    InitTouchEvents();

    airConsoleMessenger = new AirConsoleMessenger("landscape");
    airConsoleMessenger.registerEvents(onMessage);
    airConsoleMessenger.showDefaultUI(false);
});

function onMessage(fromDeviceId, message)
{
    if (message.type === "something")
    {
        airConsoleMessenger.sendMessage((new AirConsoleMessage(AirConsole.SCREEN, "something")).addPayload("something", "something"));
    }
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