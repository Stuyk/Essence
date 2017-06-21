"use strict";
API.onServerEventTrigger.connect((eventName, args) => {
    switch (eventName) {
        case "FinishLogin":
            API.callNative("_TRANSITION_FROM_BLURRED", 3000);
            resource.BrowserManager.killPanel();
            API.setGameplayCameraActive();
            return;
        case "FailLogin":
            resource.BrowserManager.callCEF("showMenu", false);
            API.sendChatMessage("Yep");
            return;
    }
});
