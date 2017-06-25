"use strict";
var actionCooldown = Date.now() + 3000;
API.onUpdate.connect(() => {
    if (API.isChatOpen()) {
        return;
    }
    if (API.isControlJustPressed(86 /* VehicleHorn */)) {
        if (Date.now() < actionCooldown) {
            return;
        }
        actionCooldown = Date.now() + 3000;
        resource.PointHelper.checkIfNearPointOnce();
    }
});
