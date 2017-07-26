var lastUpdate = Date.now();
var blips = [];
// Check if they need to be deleted through onUpdate.
API.onUpdate.connect(function () {
    if (blips.length <= 0) {
        return;
    }
    deleteTemporaryBlips();
});
// Push a blip down into our array.
API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName !== "Temp_Blip") {
        return;
    }
    lastUpdate = Date.now();
    var blip = API.createBlip(args[0]);
    blips.push(blip);
});
// Check 15 seconds from the start.
function deleteTemporaryBlips() {
    if (Date.now() < lastUpdate + 15000) {
        return;
    }
    lastUpdate = Date.now();
    for (var i = 0; i < blips.length; i++) {
        API.deleteEntity(blips[i]);
    }
    blips = [];
}
//# sourceMappingURL=TemporaryBlip.js.map