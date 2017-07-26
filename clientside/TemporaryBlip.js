"use strict";
var lastUpdate = Date.now();
var blips = [];
API.onUpdate.connect(function () {
    if (blips.length <= 0) {
        return;
    }
    deleteTemporaryBlips();
});
API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName !== "Temp_Blip") {
        return;
    }
    lastUpdate = Date.now();
    var blip = API.createBlip(args[0]);
    blips.push(blip);
});
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
