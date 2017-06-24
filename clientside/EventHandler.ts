API.onServerEventTrigger.connect((eventName, args) => {
    switch (eventName) {
        case "ShowLogin":
            resource.BrowserManager.showCEF("clientside/login.html")
            resource.BackgroundMusic.loadBackgroundMusic("www.stuyk.com/embed.html")
            return;
        case "FinishLogin":
            resource.LoginCamera.killLoginCamera();
            resource.BrowserManager.killPanel();
            API.setGameplayCameraActive();
            resource.BackgroundMusic.clearBackgroundMusic();
            API.callNative("_STOP_ALL_SCREEN_EFFECTS");
            return;
        case "FinishRegistration":
            resource.BrowserManager.callCEF("showRegistrationSuccess", false);
            return;
        case "FailLogin":
            resource.BrowserManager.callCEF("showLoginError", false);
            return;
        case "FailRegistration":
            resource.BrowserManager.callCEF("showRegistrationError", false);
            return;
        case "HeadNotification":
            resource.HeadNotifications.createHeadNotification(args[0]);
            return;
        // Used by Doors.js and Doors.cs
        case "RetrieveDoor":
            resource.Doors.retrieveDoor(args[0], args[1]);
            return;
    }
});