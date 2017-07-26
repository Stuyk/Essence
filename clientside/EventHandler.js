"use strict";
API.onServerEventTrigger.connect((eventName, args) => {
    switch (eventName) {
        case "ShowLogin":
            //resource.BrowserManager.showCEF("clientside/login.html")
            return;
        case "FinishLogin":
            resource.LoginCamera.killLoginCamera();
            resource.BrowserManager.killPanel();
            API.setGameplayCameraActive();
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
            resource.HeadNotifications.createHeadNotification(args[0]); // Just Message
            return;
        case "HeadNotificationForTarget":
            resource.HeadNotifications.createHeadNoteForTarget(args[0], args[1]); // Player Target, Message
            return;
        // Used by Doors.js and Doors.cs
        case "RetrieveDoor":
            resource.Doors.retrieveDoor(args[0], args[1]);
            return;
        // Used by PointHelper / PointHelper Manager
        case "Add_New_Point":
            resource.PointHelper.addNewPoint(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
            return;
        case "Update_Stash_Info":
            resource.PointHelper.updateStashPoint(args[0], args[1]);
            return;
        case "Play_Screen_FX":
            resource.ScreenFX.playScreenFX(args[0], args[1], args[2]);
            return;
        // LockPick
        case "End_Lock_Pick_Minigame":
            resource.Lockpick.endLockPickMiniGame();
            return;
        case "Start_Lock_Pick_Minigame":
            resource.Lockpick.newLockPickMiniGame();
            return;
        case "Continue_Lock_Pick_Minigame":
            resource.Lockpick.newLockPickMiniGame();
            return;
        // Inventory
        case "Add_Inventory_Item":
            resource.Inventory.addInventoryItem(args[0], args[1], args[2], args[3], args[4]);
            return;
    }
});
