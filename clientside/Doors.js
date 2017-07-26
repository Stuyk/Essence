"use strict";
var doors = new Array();
var lastUpdate = Date.now();
class Door {
    constructor(model, position) {
        this.model = model;
        this.position = position;
    }
    get Model() {
        return this.model;
    }
    get Position() {
        return this.position;
    }
}
function retrieveDoor(id, position) {
    var door = new Door(id, position);
    doors.push(door);
}
API.onUpdate.connect(() => {
    if (Date.now() > lastUpdate + 5000) {
        lastUpdate = Date.now();
        checkAllDoors();
    }
});
function checkAllDoors() {
    if (doors.length <= 0) {
        return;
    }
    var pos = API.getEntityPosition(API.getLocalPlayer());
    for (var i = 0; i < doors.length; i++) {
        if (doors[i].Position.DistanceTo(pos) <= 20) {
            API.callNative("SET_STATE_OF_CLOSEST_DOOR_OF_TYPE", doors[i].Model, doors[i].Position.X, doors[i].Position.Y, doors[i].Position.Z, false, 0, 1);
        }
    }
}
//# sourceMappingURL=Doors.js.map