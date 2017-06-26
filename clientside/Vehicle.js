"use strict";
var actionCooldown = Date.now() + 3000;
API.onKeyDown.connect((sender, e) => {
    if (API.isChatOpen()) {
        return;
    }
    if (e.KeyCode === Keys.E) {
        if (Date.now() < actionCooldown) {
            return;
        }
        actionCooldown = Date.now() + 3000;
        vehicleEngine();
    }
});
function vehicleEngine() {
    if (!API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        return;
    }
    API.triggerServerEvent("Vehicle_Engine");
}
