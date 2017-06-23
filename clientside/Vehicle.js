"use strict";
var actionCooldown = Date.now() + 3000;
API.onUpdate.connect(() => {
    vehicleEngine();
});
function vehicleEngine() {
    if (!API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        return;
    }
    if (API.isChatOpen()) {
        return;
    }
    API.disableControlThisFrame(86 /* VehicleHorn */);
    if (API.isDisabledControlJustPressed(86 /* VehicleHorn */)) {
        if (Date.now() < actionCooldown) {
            API.sendNotification("~r~Please wait 3 seconds before sending another command.");
            return;
        }
        actionCooldown = Date.now() + 3000;
        API.triggerServerEvent("Vehicle_Engine");
    }
}
