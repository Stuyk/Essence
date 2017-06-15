var screenX = API.getScreenResolutionMantainRatio().Width;
var screenY = API.getScreenResolutionMantainRatio().Height;

var loggedIn = false;
var money = 0;
var zone = "";
var zoneUpdate = Date.now(); //ms

API.onEntityDataChange.connect(function(entity, key, oldValue) {
    if (API.getLocalPlayer().Value !== entity.Value) {
        return;
    }

    switch (key) {
        case "ESS_Money":
            money = API.getEntitySyncedData(API.getLocalPlayer(), "ESS_Money");
            return;
        case "ESS_LoggedIn":
            loggedIn = true;
            return;
    }
});

API.onUpdate.connect(function () {
    if (!loggedIn) {
        return;
    }

    drawMoney();
    drawZone();
});

/**
 *  Display the players on-hand money.
 */
function drawMoney() {
    API.drawText(`$${money}`, 35, screenY - 225, 0.5, 85, 107, 47, 255, 7, 0, false, true, 300);
}

/**
 * Display the players current zone location.
 */
function drawZone() {
    API.drawText(`${zone}`, 315, screenY - 45, 0.5, 85, 107, 47, 255, 7, 0, false, true, 300);
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




