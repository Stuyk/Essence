var screenX = API.getScreenResolutionMantainRatio().Width;
var screenY = API.getScreenResolutionMantainRatio().Height;


// Other Stuff
var loggedIn = false;
var money = 0;
var zone = "";
var zoneUpdate = Date.now(); //ms

// Logo Fade
var logoFade = 255;

//
var startTransition = false;

API.onEntityDataChange.connect(function(entity, key, oldValue) {
    if (API.getLocalPlayer().Value !== entity.Value) {
        return;
    }

    switch (key) {
        case "ESS_Money":
            money = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
            return;
        case "ESS_LoggedIn":
            API.callNative("_TRANSITION_FROM_BLURRED", 4000);
            API.callNative("DO_SCREEN_FADE_OUT", 3000);
            startTransition = true;
            return;
    }
});

API.onResourceStart.connect(() => {
    // Basic Setup for HUD.
    API.callNative("_TRANSITION_TO_BLURRED", 3000);
    API.setHudVisible(false);
    var cam = API.createCamera(API.getEntityPosition(API.getLocalPlayer()), new Vector3())
    API.setCameraFov(cam, 110);
    API.setCameraPosition(cam, API.getCameraPosition(cam).Add(new Vector3(0, 0, 100)));
    API.setActiveCamera(cam);
});

API.onResourceStop.connect(() => {
    API.callNative("_TRANSITION_FROM_BLURRED", 3000);
    API.setHudVisible(true);
});

API.onUpdate.connect(function () {
    if (!loggedIn && logoFade >= 1 && startTransition) {
        API.dxDrawTexture("clientside/images/essence.png", new Point(Math.round(screenX / 2 - 373), Math.round(screenY / 2 - 57)), new Size(746, 115), 0, 255, 255, 255, Math.round(logoFade));
        logoFade -= 0.6;
        if (logoFade <= 1) {
            loggedIn = true;
            API.setHudVisible(true);
            API.callNative("DO_SCREEN_FADE_IN", 3000);
            API.setGameplayCameraActive();
            return;
        }
        return;
    }

    drawMoney();
    drawZone();
});

/**
 *  Display the players on-hand money.
 */
function drawMoney() {
    API.drawText(`$${money}`, 35, screenY - 225, 0.5, 129, 199, 132, 255, 7, 0, false, true, 300);
}

/**
 * Display the players current zone location.
 */
function drawZone() {
    API.drawText(`${zone}`, 315, screenY - 45, 0.5, 77, 208, 225, 255, 7, 0, false, true, 300);
    updateZone();
}

/**
 * Update the players current zone location.
 */
function updateZone() {
    if (Date.now() < zoneUpdate + 10000) {
        return;
    }
    zoneUpdate = Date.now();
    var location = API.getEntityPosition(API.getLocalPlayer());
    zone = API.getZoneName(location);
}




