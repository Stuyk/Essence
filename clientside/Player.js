var actionCooldown = Date.now() + 3000;
API.onKeyDown.connect(function (sender, e) {
    if (API.isChatOpen()) {
        return;
    }
    if (e.KeyCode === Keys.E) {
        if (Date.now() < actionCooldown) {
            return;
        }
        actionCooldown = Date.now() + 3000;
        resource.PointHelper.checkIfNearPointOnce();
    }
});
//# sourceMappingURL=Player.js.map