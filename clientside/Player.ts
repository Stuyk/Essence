var actionCooldown: number = Date.now() + 3000;

API.onUpdate.connect(() => {
    if (API.isChatOpen()) {
        return;
    }

    if (API.isControlJustPressed(Enums.Controls.VehicleHorn)) {
        if (Date.now() < actionCooldown) {
            return;
        }
        actionCooldown = Date.now() + 3000;
        resource.PointHelper.checkIfNearPointOnce();
    }
});